using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.DTOs.APIs;
using mdr_server.DTOs.Study;
using mdr_server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace mdr_server.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class ApiController : Controller
    {
        private readonly IApiRepository _apiRepository;
        public ApiController(IApiRepository apiRepository)
        {
            _apiRepository = apiRepository;
        }

        [HttpPost("specific-study")]
        public async Task<List<StudyDto>> SpecificStudyApi(ApiSpecificStudyDto apiSpecificStudyDto)
        {
            return await _apiRepository.SpecificStudyApi(apiSpecificStudyDto);
        }
        
        [HttpPost("study-characteristics")]
        public async Task<List<StudyDto>> ByStudyCharacteristicsApi(ApiStudyCharacteristicsDto apiStudyCharacteristicsDto)
        {
            return await _apiRepository.ByStudyCharacteristicsApi(apiStudyCharacteristicsDto);
        }
        
        [HttpPost("via-published-paper")]
        public async Task<List<StudyDto>> ViaPublishedPaperApi(ApiViaPublishedPaperDto apiViaPublishedPaperDto)
        {
            return await _apiRepository.ViaPublishedPaperApi(apiViaPublishedPaperDto);
        }
        
        [HttpPost("study")]
        public async Task<List<StudyDto>> ByStudyIdApi(ApiStudyIdDto apiStudyIdDto)
        {
            return await _apiRepository.ByStudyIdApi(apiStudyIdDto);
        }
    }
}