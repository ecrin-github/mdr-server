using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mdr_server.Entities.Object;
using mdr_server.Interfaces;
using Nest;

namespace mdr_server.Data
{
    public class ObjectRepository : IObjectRepository
    {
        private readonly IElasticSearchService _elasticSearchService;

        public ObjectRepository(IElasticSearchService elasticSearchService)
        {
            _elasticSearchService = elasticSearchService;
        }
        public async Task<List<Object>> GetFetchedObjects(int[] ids)
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
            return result.Documents.ToList();
        }
    }
}