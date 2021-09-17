using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using mdr_server.Contracts.v1.Requests.Query;
using mdr_server.Contracts.v1.Responses;
using mdr_server.Contracts.v1.Responses.StudyListResponse;
using mdr_server.Entities.FetchedData;
using mdr_server.Entities.Study;
using mdr_server.Interfaces;
using Nest;
using Object = mdr_server.Entities.Object.Object;

namespace mdr_server.Data
{
    public class QueryRepository : IQueryRepository
    {
        private readonly IElasticSearchService _elasticSearchService;
        private readonly IDataMapper _dataMapper;

        public QueryRepository(IElasticSearchService elasticSearchService, IDataMapper dataMapper)
        {
            _elasticSearchService = elasticSearchService;
            _dataMapper = dataMapper;
        }

        private static int? CalculateStartFrom(int? page, int? pageSize)
        {
            if (page != null && pageSize == null) return null;
            var startFrom = ((page + 1) * pageSize) - pageSize;
            if (startFrom == 1 && pageSize == 1)
            {
                startFrom = 0;
            }
            return startFrom;
        }

        private static bool HasProperty(object obj, string propertyName)
        {
            if (obj == null) return false;
            if (obj.GetType().GetProperty(propertyName) != null) return true;
            return false;
        }

        public async Task<BaseResponse> GetSpecificStudy(SpecificStudyRequest specificStudyRequest)
        {
            var startFrom = CalculateStartFrom(specificStudyRequest.Page, specificStudyRequest.Size);

            string identifierValue = specificStudyRequest.SearchValue.ToUpper().Trim();

            FiltersListRequest filtersListRequest = null;

            List<QueryContainer> filters = null;
            if (HasProperty(specificStudyRequest, "Filters") && specificStudyRequest.Filters != null)
            {
                filtersListRequest = specificStudyRequest.Filters;
                if (HasProperty(specificStudyRequest.Filters, "StudyFilters"))
                {
                    filters = new List<QueryContainer>();
                    foreach (var param in specificStudyRequest.Filters!.StudyFilters)
                    {
                        filters.Add(new RawQuery(JsonSerializer.Serialize(param)));
                    }
                }
            }
            
            var queryClause = new List<QueryContainer>();
            
            queryClause.Add(new NestedQuery()
            {
                Name = "",
                Path = new Field("study_identifiers"),
                Query = new TermQuery()
                        {
                            Field = Infer.Field<Study>(p => p.StudyIdentifiers.First()
                                .IdentifierType.Id), Value = specificStudyRequest.SearchType
                        } &&
                        new TermQuery()
                        {
                            Field = Infer.Field<Study>(p => p.StudyIdentifiers.First()
                                .IdentifierValue), Value = identifierValue
                        }
            });
            
            if (filters is { Count: > 0 })
            {
                queryClause.Add(new BoolQuery()
                {
                    Should = filters
                });
            }

            var boolQuery = new BoolQuery()
            {
                Must = queryClause
            };
            
            SearchRequest<Study> searchRequest;
            if (startFrom != null)
            {
                searchRequest = new SearchRequest<Study>(Indices.Index("study"))
                {
                    From = startFrom,
                    Size = specificStudyRequest.Size,
                    Query = boolQuery
                };
            }
            else
            {
                searchRequest = new SearchRequest<Study>(Indices.Index("study"))
                {
                    Query = boolQuery
                };
            }
            
            var results = await _elasticSearchService.GetConnection().SearchAsync<Study>(searchRequest);
            FetchedStudies fetchedStudies = new FetchedStudies()
            {
                Total = results.Total,
                Studies = results.Documents.ToList()
            };
            var studies = await _dataMapper.MapStudies(fetchedStudies, filtersListRequest);
            return new BaseResponse()
            {
                Total = results.Total,
                Data = studies
            };
            
        }

        public async Task<BaseResponse> GetByStudyCharacteristics(StudyCharacteristicsRequest studyCharacteristicsRequest)
        {
            var startFrom = CalculateStartFrom(studyCharacteristicsRequest.Page, studyCharacteristicsRequest.Size);
            
            FiltersListRequest filtersListRequest = null;
            
            List<QueryContainer> filters = null;
            if (HasProperty(studyCharacteristicsRequest, "Filters") && studyCharacteristicsRequest.Filters != null)
            {
                filtersListRequest = studyCharacteristicsRequest.Filters;
                if (HasProperty(studyCharacteristicsRequest.Filters, "StudyFilters"))
                {
                    filters = new List<QueryContainer>();
                    foreach (var param in filtersListRequest.StudyFilters)
                    {
                        filters.Add(new RawQuery(JsonSerializer.Serialize(param)));
                    }
                }
            }

            var queryClauses = new List<QueryContainer>();

            if (!string.IsNullOrEmpty(studyCharacteristicsRequest.TitleContains))
            {
                queryClauses.Add(new SimpleQueryStringQuery()
                {
                    Fields = Infer.Field<Study>(f => f.DisplayTitle),
                    Query = studyCharacteristicsRequest.TitleContains,
                    DefaultOperator = Operator.And
                });
                queryClauses.Add(new NestedQuery()
                {
                    Path = Infer.Field<Study>(p => p.StudyTitles),
                    Query = new SimpleQueryStringQuery()
                    {
                        Fields = Infer.Field<Study>(f => f.StudyTitles.First().TitleText),
                        Query = studyCharacteristicsRequest.TitleContains,
                        DefaultOperator = Operator.And
                    }
                });
            }

            if (!string.IsNullOrEmpty(studyCharacteristicsRequest.TopicsInclude))
            {
                queryClauses.Add(new NestedQuery()
                {
                    Path = Infer.Field<Study>(p => p.StudyTopics),
                    Query = new SimpleQueryStringQuery()
                    {
                        Fields = Infer.Field<Study>(f => f.StudyTopics.First().MeshValue).And("original_value"),
                        Query = studyCharacteristicsRequest.TopicsInclude,
                        DefaultOperator = Operator.And
                    }
                });
            }

            if (filters is { Count: > 0 })
            {
                queryClauses.Add(new BoolQuery()
                {
                    Should = filters
                });
            }

            string logicalOperator = studyCharacteristicsRequest.LogicalOperator;

            if (string.IsNullOrEmpty(logicalOperator))
            {
                logicalOperator = "and";
            }

            BoolQuery boolQuery;
            if (logicalOperator == "and")
            {
                boolQuery = new BoolQuery()
                {
                    Must = queryClauses
                };
            }
            else
            {
                boolQuery = new BoolQuery()
                {
                    Should = queryClauses
                };
            }

            SearchRequest<Study> searchRequest;
            if (startFrom != null)
            {
                searchRequest = new SearchRequest<Study>(Indices.Index("study"))
                {
                    From = startFrom,
                    Size = studyCharacteristicsRequest.Size,
                    Query = boolQuery
                };
            }
            else
            {
                searchRequest = new SearchRequest<Study>(Indices.Index("study"))
                {
                    Query = boolQuery
                };
            }
            
            {
                var results = await _elasticSearchService.GetConnection().SearchAsync<Study>(searchRequest);
                FetchedStudies fetchedStudies = new FetchedStudies()
                {
                    Total = results.Total,
                    Studies = results.Documents.ToList()
                };
                var studies = await _dataMapper.MapStudies(fetchedStudies, filtersListRequest);
                return new BaseResponse()
                {
                    Total = results.Total,
                    Data = studies
                };
            }
        }

        public async Task<BaseResponse> GetViaPublishedPaper(ViaPublishedPaperRequest viaPublishedPaperRequest)
        {
            var startFrom = CalculateStartFrom(viaPublishedPaperRequest.Page, viaPublishedPaperRequest.Size);

            FiltersListRequest filtersListRequest = null;
            
            List<QueryContainer> filters = null;
            if (HasProperty(viaPublishedPaperRequest, "Filters") && viaPublishedPaperRequest.Filters != null)
            {
                filtersListRequest = viaPublishedPaperRequest.Filters;
                if (HasProperty(viaPublishedPaperRequest.Filters, "ObjectFilters"))
                {
                    filters = new List<QueryContainer>();
                    foreach (var param in filtersListRequest.ObjectFilters)
                    {
                        filters.Add(new RawQuery(JsonSerializer.Serialize(param)));
                    }
                }
            }
            
            var mustQuery = new List<QueryContainer>();
            
            if (viaPublishedPaperRequest.SearchType == "doi")
            {
                mustQuery.Add(new TermQuery()
                {
                    Field = Infer.Field<Object>(p => p.Doi),
                    Value = viaPublishedPaperRequest.SearchValue
                });
            }
            else
            {
                var shouldClauses = new List<QueryContainer>();
                shouldClauses.Add(new SimpleQueryStringQuery()
                {
                    Query = viaPublishedPaperRequest.SearchValue,
                    Fields = Infer.Field<Object>(p => p.DisplayTitle),
                    DefaultOperator = Operator.And
                });
                shouldClauses.Add(new NestedQuery()
                {
                    Path = Infer.Field<Object>(p => p.ObjectTitles),
                    Query = new SimpleQueryStringQuery()
                    {
                        Query = viaPublishedPaperRequest.SearchValue,
                        Fields = Infer.Field<Object>(p => p.ObjectTitles.First().TitleText),
                        DefaultOperator = Operator.And
                    }
                });
                mustQuery.Add(new BoolQuery()
                {
                    Should = shouldClauses
                });
            }
            
            if (filters is { Count: > 0 })
            {
                mustQuery.Add(new BoolQuery()
                {
                    Should = filters
                });
            }

            var boolQuery = new BoolQuery()
            {
                Must = mustQuery
            };
            
            SearchRequest<Object> searchRequest;
            if (startFrom != null)
            {
                searchRequest = new SearchRequest<Object>(Indices.Index("data-object"))
                {
                    From = startFrom,
                    Size = viaPublishedPaperRequest.Size,
                    Query = boolQuery
                };
            }
            else
            {
                searchRequest = new SearchRequest<Object>(Indices.Index("data-object"))
                {
                    Query = boolQuery
                };
            }
            
            {
                var results = await _elasticSearchService.GetConnection().SearchAsync<Object>(searchRequest);
                
                /*FetchedObjects fetchedObjects = new FetchedObjects()
                {
                    Total = results.Total,
                    Objects = results.Documents.ToList()
                };
                var studies = await _dataMapper.MapObjects(fetchedObjects, filtersListRequest);

                return new BaseResponse()
                {
                    Total = results.Total,
                    Data = studies
                };*/

                var studies = await _dataMapper.MapViaPublishedPaper(results.Documents.ToList());

                return new BaseResponse()
                {
                    Total = results.Total,
                    Data = studies
                };
            }
        }

        public async Task<IEnumerable<StudyListResponse>> GetByStudyId(StudyIdRequest studyIdRequest)
        {
            var results = await _elasticSearchService.GetConnection().SearchAsync<Study>(s => s
                .Index("study")
                .From(0)
                .Size(1)
                .Query(q => q
                    .Term(t => t
                        .Field(p => p.Id)
                        .Value(studyIdRequest.StudyId.ToString())
                    )
                )
            );
            return await _dataMapper.MapStudy(results.Documents.ToList());
        }
    }
}