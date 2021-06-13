using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mdr_server.Contracts.v1.Requests.Query;
using mdr_server.Contracts.v1.Responses;
using mdr_server.Entities.Object;
using mdr_server.Entities.Study;
using mdr_server.Entities.Types;
using mdr_server.Interfaces;
using Nest;

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

        private string GetIdentifierType(int id)
        {
            var identifierTypes = new List<IdentifierType>();
            
            identifierTypes.Add(new IdentifierType(){Id = 11, Name = "Trial registry ID"});
            identifierTypes.Add(new IdentifierType(){Id = 41, Name = "Regulatory body ID"});
            identifierTypes.Add(new IdentifierType(){Id = 12, Name = "Ethics review ID"});
            identifierTypes.Add(new IdentifierType(){Id = 13, Name = "Funder's ID"});
            identifierTypes.Add(new IdentifierType(){Id = 14, Name = "Sponsor's ID"});
            identifierTypes.Add(new IdentifierType(){Id = 39, Name = "NIH CTRP ID"});
            identifierTypes.Add(new IdentifierType(){Id = 40, Name = "DAIDS ID"});
            identifierTypes.Add(new IdentifierType(){Id = 42, Name = "NHLBI ID"});
            
            return identifierTypes.Find(x => x.Id == id)?.Name;
        }
        
        public async Task<BaseResponse> GetSpecificStudy(SpecificStudyRequest specificStudyRequest)
        {
            var startFrom = CalculateStartFrom(specificStudyRequest.Page, specificStudyRequest.PageSize);

            var identifierType = GetIdentifierType(specificStudyRequest.SearchType);
            
            SearchRequest<Study> searchRequest;
            if (startFrom != null)
            {
                searchRequest = new SearchRequest<Study>(Indices.Index("study"))
                {
                    From = startFrom,
                    Size = specificStudyRequest.PageSize,
                    Query = new NestedQuery()
                    {
                        Name = "",
                        Path = new Field("study_identifiers"),
                        Query = new TermQuery() {Field = Infer.Field<Study>(p => p.StudyIdentifiers.First().IdentifierType), Value = identifierType} &&
                                new TermQuery() {Field = Infer.Field<Study>(p => p.StudyIdentifiers.First().IdentifierValue), Value = specificStudyRequest.SearchValue}
                    }
                };
            }
            else
            {
                searchRequest = new SearchRequest<Study>(Indices.Index("study"))
                {
                    Query = new NestedQuery()
                    {
                        Name = "",
                        Path = new Field("study_identifiers"),
                        Query = new TermQuery() {Field = new Field("study_identifiers.identifier_type"), Value = identifierType} &&
                                new TermQuery() {Field = new Field("study_identifiers.identifier_value"), Value = specificStudyRequest.SearchValue}
                    }
                };
            }
            
            var results = await _elasticSearchService.GetConnection().SearchAsync<Study>(searchRequest);
            var total = results.Total;
            var studies = await _dataMapper.MapStudies(results.Documents.ToList());
            return new BaseResponse()
            {
                Total = total,
                Data = studies
            };
            
        }

        public async Task<BaseResponse> GetByStudyCharacteristics(StudyCharacteristicsRequest studyCharacteristicsRequest)
        {
            var startFrom = CalculateStartFrom(studyCharacteristicsRequest.Page, studyCharacteristicsRequest.PageSize);

            var shouldClause = new List<QueryContainer>();
            shouldClause.Add(new SimpleQueryStringQuery()
            {
                Fields = Infer.Field<Study>(f => f.DisplayTitle),
                Query = studyCharacteristicsRequest.TitleContains,
                DefaultOperator = Operator.And
            });
            shouldClause.Add(new NestedQuery()
            {
                Path = Infer.Field<Study>(p => p.StudyTitles),
                Query = new SimpleQueryStringQuery()
                {
                    Fields = Infer.Field<Study>(f => f.StudyTitles.First().TitleText),
                    Query = studyCharacteristicsRequest.TitleContains,
                    DefaultOperator = Operator.And
                }
            });

            var queryClauses = new List<QueryContainer>();
            queryClauses.Add(new BoolQuery()
            {
                Should = shouldClause
            });

            if (!string.IsNullOrEmpty(studyCharacteristicsRequest.TopicsInclude))
            {
                queryClauses.Add(new NestedQuery()
                {
                    Path = Infer.Field<Study>(p => p.StudyTopics),
                    Query = new SimpleQueryStringQuery()
                    {
                        Fields = Infer.Field<Study>(f => f.StudyTopics.First().TopicValue),
                        Query = studyCharacteristicsRequest.TopicsInclude,
                        DefaultOperator = Operator.And
                    }
                });
            }
            
            var boolQuery = new BoolQuery();
            if (studyCharacteristicsRequest.LogicalOperator == "and")
            {
                boolQuery.Must = queryClauses;
            }
            else
            {
                boolQuery.Should = queryClauses;
            }

            SearchRequest<Study> searchRequest;
            if (startFrom != null)
            {
                searchRequest = new SearchRequest<Study>(Indices.Index("study"))
                {
                    From = startFrom,
                    Size = studyCharacteristicsRequest.PageSize,
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
                var total = results.Total;
                var studies = await _dataMapper.MapStudies(results.Documents.ToList());
                return new BaseResponse()
                {
                    Total = total,
                    Data = studies
                };
            }
        }

        public async Task<BaseResponse> GetViaPublishedPaper(ViaPublishedPaperRequest viaPublishedPaperRequest)
        {
            var startFrom = CalculateStartFrom(viaPublishedPaperRequest.Page, viaPublishedPaperRequest.PageSize);

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
            
            SearchRequest<Object> searchRequest;
            if (startFrom != null)
            {
                searchRequest = new SearchRequest<Object>(Indices.Index("data-object"))
                {
                    From = startFrom,
                    Size = viaPublishedPaperRequest.PageSize,
                    Query = new BoolQuery()
                    {
                        Must = mustQuery
                    }
                };
            }
            else
            {
                searchRequest = new SearchRequest<Object>(Indices.Index("data-object"))
                {
                    Query = new BoolQuery()
                    {
                        Must = mustQuery
                    }
                };
            }
            
            {
                var results = await _elasticSearchService.GetConnection().SearchAsync<Object>(searchRequest);
                var studies = await _dataMapper.MapObjects(results.Documents.ToList());

                return new BaseResponse()
                {
                    Total = studies.Count,
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
            var studies = results.Documents.ToList();

            return await _dataMapper.MapStudies(studies);
        }
    }
}