using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.DTOs.Queries;
using mdr_server.DTOs.Study;
using mdr_server.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace mdr_server.Controllers
{
    [ApiController]
    [Route("")]
    public class SearchApiController : ControllerBase
    {

        private readonly ISearchRepository _searchRepository;

        public SearchApiController(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        [HttpPost]
        [Route("search-study")]
        public async Task<List<StudyDto>> GetStudySearchResults(ElasticQueryDto elasticQueryDto)
        {
            return await _searchRepository.GetStudySearchResults(elasticQueryDto);
        }
        
        [HttpPost]
        [Route("search-object")]
        public async Task<List<StudyDto>> GetObjectSearchResults(ElasticQueryDto elasticQueryDto)
        {
            return await _searchRepository.GetObjectSearchResults(elasticQueryDto);
        }
    }
}