using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mdr_server.Entities.Study;
using mdr_server.Interfaces;

namespace mdr_server.Data
{
    public class StudyRepository : IStudyRepository
    {
        private readonly IElasticSearchService _elasticSearchService;

        public StudyRepository(IElasticSearchService elasticSearchService)
        {
            _elasticSearchService = elasticSearchService;
        }
        
        public async Task<List<Study>> GetFetchedStudies(int[] ids)
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
            return result.Documents.ToList();
        }
    }
}