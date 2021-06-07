using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.DTOs.APIs;
using mdr_server.DTOs.Study;
using mdr_server.Interfaces;

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
        
        public Task<List<StudyDto>> SpecificStudyApi(ApiSpecificStudyDto apiSpecificStudyDto)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<StudyDto>> ByStudyCharacteristicsApi(ApiStudyCharacteristicsDto apiStudyCharacteristicsDto)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<StudyDto>> ViaPublishedPaperApi(ApiViaPublishedPaperDto apiViaPublishedPaperDto)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<StudyDto>> ByStudyIdApi(ApiStudyIdDto apiStudyIdDto)
        {
            throw new System.NotImplementedException();
        }
    }
}