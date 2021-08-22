using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using mdr_server.Contracts.v1.Requests.Query;
using mdr_server.Entities.FetchedData;
using mdr_server.Entities.Study;
using mdr_server.Interfaces;
using Nest;
using Object = mdr_server.Entities.Object.Object;

namespace mdr_server.Data
{
    public class FetchedDataRepository : IFetchedDataRepository
    {
        private readonly IElasticSearchService _elasticSearchService;

        public FetchedDataRepository(IElasticSearchService elasticSearchService)
        {
            _elasticSearchService = elasticSearchService;
        }
        
        private static bool HasProperty(object obj, string propertyName)
        {
            if (obj.GetType().GetProperty(propertyName) != null) return true;
            return false;
        }
        
        public async Task<FetchedObjects> GetFetchedObjects(int[] ids, FiltersListRequest filtersListRequest)
        {

            List<QueryContainer> mustNot = null;
            if (filtersListRequest != null)
            {
                if (HasProperty(filtersListRequest, "ObjectFilters"))
                {
                    mustNot = new List<QueryContainer>();
                    foreach (var param in filtersListRequest.ObjectFilters)
                    {
                        mustNot.Add(new RawQuery(JsonSerializer.Serialize(param)));
                    }
                }
            }

            var objectArray = ids.Cast<object>().ToArray();

            var queryClause = new List<QueryContainer>();
            queryClause.Add(new TermsQuery()
            {
                Field = Infer.Field<Object>(p => p.Id),
                Terms = objectArray
            });
            
            BoolQuery boolQuery;
            if (mustNot == null)
            {
                boolQuery = new BoolQuery()
                {
                    Filter = queryClause
                };
            }
            else
            {
                boolQuery = new BoolQuery()
                {
                    Filter = queryClause,
                    MustNot = mustNot
                };
            }

            SearchRequest<Object> searchRequest = new SearchRequest<Object>(Indices.Index("data-object"))
            {
                Query = boolQuery
            };

            var result = await _elasticSearchService.GetConnection().SearchAsync<Object>(searchRequest);
            FetchedObjects fetchedObjects = new FetchedObjects()
            {
                Total = result.Total,
                Objects = result.Documents.ToList()
            };
            return fetchedObjects;
        }
        
        public async Task<FetchedStudies> GetFetchedStudies(int[] ids, FiltersListRequest filtersListRequest)
        {
            List<QueryContainer> mustNot = null;
            if (filtersListRequest != null)
            {
                if (HasProperty(filtersListRequest, "StudyFilters"))
                {
                    mustNot = new List<QueryContainer>();
                    foreach (var param in filtersListRequest.StudyFilters)
                    {
                        mustNot.Add(new RawQuery(JsonSerializer.Serialize(param)));
                    }
                }
            }
            
            var objectArray = ids.Cast<object>().ToArray();

            var queryClause = new List<QueryContainer>();
            queryClause.Add(new TermsQuery()
            {
                Field = Infer.Field<Study>(p => p.Id),
                Terms = objectArray
            });
            
            BoolQuery boolQuery;
            if (mustNot == null)
            {
                boolQuery = new BoolQuery()
                {
                    Filter = queryClause
                };
            }
            else
            {
                boolQuery = new BoolQuery()
                {
                    Filter = queryClause,
                    MustNot = mustNot
                };
            }

            SearchRequest<Study> searchRequest = new SearchRequest<Study>(Indices.Index("study"))
            {
                Query = boolQuery,
            };
            
            var result = await _elasticSearchService.GetConnection().SearchAsync<Study>(searchRequest);
            FetchedStudies fetchedStudies = new FetchedStudies()
            {
                Total = result.Total,
                Studies = result.Documents.ToList()
            };
            return fetchedStudies;
        }


        public async Task<FetchedObjects> GetStudyObjects(int[] ids)
        {
            var result = await _elasticSearchService.GetConnection().SearchAsync<Object>(s => s
                .Index("data-object")
                .Query(q => q
                    .Bool(b => b
                        .Filter(f => f
                            .Terms(t => t
                                .Field(p => p.Id)
                                .Terms(ids)
                            )
                        )
                    )
                )
            );
            FetchedObjects fetchedObjects = new FetchedObjects()
            {
                Total = result.Total,
                Objects = result.Documents.ToList()
            };
            return fetchedObjects;
        }
        
        
        public async Task<FetchedStudies> GetObjectStudies(int[] ids)
        {
            var result = await _elasticSearchService.GetConnection().SearchAsync<Study>(s => s
                .Index("study")
                .Query(q => q
                    .Bool(b => b
                        .Filter(f => f
                            .Terms(t => t
                                .Field(p => p.Id)
                                .Terms(ids)
                            )
                        )
                    )
                )
            );
            FetchedStudies fetchedStudies = new FetchedStudies()
            {
                Total = result.Total,
                Studies = result.Documents.ToList()
            };
            return fetchedStudies;
        }
    }
}