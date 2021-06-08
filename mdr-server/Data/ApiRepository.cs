using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mdr_server.DTOs.APIs;
using mdr_server.DTOs.Study;
using mdr_server.Entities.Object;
using mdr_server.Entities.Study;
using mdr_server.Entities.Types;
using mdr_server.Interfaces;
using Nest;

namespace mdr_server.Data
{
    public class ApiRepository : IApiRepository
    {
        private readonly IElasticSearchService _elasticSearchService;
        private readonly IDataMapper _dataMapper;

        public ApiRepository(IElasticSearchService elasticSearchService, IDataMapper dataMapper)
        {
            _elasticSearchService = elasticSearchService;
            _dataMapper = dataMapper;
        }

        private static int? CalculateStartFrom(int? page, int? pageSize)
        {
            if (page == null || pageSize == null) return null;
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
        
        public async Task<List<StudyDto>> SpecificStudyApi(ApiSpecificStudyDto apiSpecificStudyDto)
        {
            var startFrom = CalculateStartFrom(apiSpecificStudyDto.Page, apiSpecificStudyDto.PageSize);

            var identifierType = GetIdentifierType(apiSpecificStudyDto.SearchType);
            
            SearchRequest<Study> searchRequest;
            if (startFrom != null)
            {
                searchRequest = new SearchRequest<Study>(Indices.Index("study"))
                {
                    From = startFrom,
                    Size = apiSpecificStudyDto.PageSize,
                    Query = new NestedQuery()
                    {
                        Name = "",
                        Path = new Field("study_identifiers"),
                        Query = new TermQuery() {Field = Infer.Field<Study>(p => p.StudyIdentifiers.First().IdentifierType), Value = identifierType} &&
                                new TermQuery() {Field = Infer.Field<Study>(p => p.StudyIdentifiers.First().IdentifierValue), Value = apiSpecificStudyDto.SearchValue}
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
                                new TermQuery() {Field = new Field("study_identifiers.identifier_value"), Value = apiSpecificStudyDto.SearchValue}
                    }
                };
            }
            
            var results = await _elasticSearchService.GetConnection().SearchAsync<Study>(searchRequest);
            var studies = results.Documents.ToList();

            return await _dataMapper.MapStudies(studies);
            
        }

        public async Task<List<StudyDto>> ByStudyCharacteristicsApi(ApiStudyCharacteristicsDto apiStudyCharacteristicsDto)
        {
            var startFrom = CalculateStartFrom(apiStudyCharacteristicsDto.Page, apiStudyCharacteristicsDto.PageSize);

            var shouldClause = new List<QueryContainer>();
            shouldClause.Add(new SimpleQueryStringQuery()
            {
                Fields = Infer.Field<Study>(f => f.DisplayTitle),
                Query = apiStudyCharacteristicsDto.TitleContains,
                DefaultOperator = Operator.And
            });
            shouldClause.Add(new NestedQuery()
            {
                Path = Infer.Field<Study>(p => p.StudyTitles),
                Query = new SimpleQueryStringQuery()
                {
                    Fields = Infer.Field<Study>(f => f.StudyTitles.First().TitleText),
                    Query = apiStudyCharacteristicsDto.TitleContains,
                    DefaultOperator = Operator.And
                }
            });

            var queryClauses = new List<QueryContainer>();
            queryClauses.Add(new BoolQuery()
            {
                Should = shouldClause
            });

            if (!string.IsNullOrEmpty(apiStudyCharacteristicsDto.TopicsInclude))
            {
                queryClauses.Add(new NestedQuery()
                {
                    Path = Infer.Field<Study>(p => p.StudyTopics),
                    Query = new SimpleQueryStringQuery()
                    {
                        Fields = Infer.Field<Study>(f => f.StudyTopics.First().TopicValue),
                        Query = apiStudyCharacteristicsDto.TopicsInclude,
                        DefaultOperator = Operator.And
                    }
                });
            }
            
            var boolQuery = new BoolQuery();
            if (apiStudyCharacteristicsDto.LogicalOperator == "and")
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
                    Size = apiStudyCharacteristicsDto.PageSize,
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
                var studies = results.Documents.ToList();

                return await _dataMapper.MapStudies(studies);
            }
        }

        public async Task<List<StudyDto>> ViaPublishedPaperApi(ApiViaPublishedPaperDto apiViaPublishedPaperDto)
        {
            var startFrom = CalculateStartFrom(apiViaPublishedPaperDto.Page, apiViaPublishedPaperDto.PageSize);

            var mustQuery = new List<QueryContainer>();
            
            if (apiViaPublishedPaperDto.SearchType == "doi")
            {
                mustQuery.Add(new TermQuery()
                {
                    Field = Infer.Field<Object>(p => p.Doi),
                    Value = apiViaPublishedPaperDto.SearchValue
                });
            }
            else
            {
                var shouldClauses = new List<QueryContainer>();
                shouldClauses.Add(new SimpleQueryStringQuery()
                {
                    Query = apiViaPublishedPaperDto.SearchValue,
                    Fields = Infer.Field<Object>(p => p.DisplayTitle),
                    DefaultOperator = Operator.And
                });
                shouldClauses.Add(new NestedQuery()
                {
                    Path = Infer.Field<Object>(p => p.ObjectTitles),
                    Query = new SimpleQueryStringQuery()
                    {
                        Query = apiViaPublishedPaperDto.SearchValue,
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
                    Size = apiViaPublishedPaperDto.PageSize,
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
                var objects = results.Documents.ToList();

                return await _dataMapper.MapObjects(objects);
            }
        }

        public async Task<List<StudyDto>> ByStudyIdApi(ApiStudyIdDto apiStudyIdDto)
        {
            var results = await _elasticSearchService.GetConnection().SearchAsync<Study>(s => s
                .Index("study")
                .From(0)
                .Size(1)
                .Query(q => q
                    .Term(t => t
                        .Field(p => p.Id)
                        .Value(apiStudyIdDto.StudyId.ToString())
                    )
                )
            );
            var studies = results.Documents.ToList();

            return await _dataMapper.MapStudies(studies);
        }
    }
}