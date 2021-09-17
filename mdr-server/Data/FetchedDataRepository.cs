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
            if (obj == null) return false;
            if (obj.GetType().GetProperty(propertyName) != null) return true;
            return false;
        }
        
        public async Task<FetchedObjects> GetFetchedObjects(int[] ids, FiltersListRequest filtersListRequest)
        {

            List<QueryContainer> filters = null;
            if (filtersListRequest != null)
            {
                if (HasProperty(filtersListRequest, "ObjectFilters"))
                {
                    filters = new List<QueryContainer>();
                    foreach (var param in filtersListRequest.ObjectFilters)
                    {
                        filters.Add(new RawQuery(JsonSerializer.Serialize(param)));
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

            if (filters is { Count: > 0 })
            {
                queryClause.Add(new BoolQuery()
                {
                    Should = filters
                });
            }
            
            SearchRequest<Object> searchRequest = new SearchRequest<Object>(Indices.Index("data-object"))
            {
                Query = new BoolQuery()
                {
                    Filter = queryClause
                }
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
            List<QueryContainer> filters = null;
            if (filtersListRequest != null)
            {
                if (HasProperty(filtersListRequest, "StudyFilters"))
                {
                    filters = new List<QueryContainer>();
                    foreach (var param in filtersListRequest.StudyFilters)
                    {
                        filters.Add(new RawQuery(JsonSerializer.Serialize(param)));
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
            
            if (filters is { Count: > 0 })
            {
                queryClause.Add(new BoolQuery()
                {
                    Should = filters
                });
            }

            SearchRequest<Study> searchRequest = new SearchRequest<Study>(Indices.Index("study"))
            {
                Query = new BoolQuery()
                {
                    Filter = queryClause
                },
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