using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.DTOs.Queries;
using mdr_server.DTOs.Study;
using mdr_server.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace mdr_server.Controllers
{
    [ApiController]
    [Route("search-api")]
    public class SearchApiController : ControllerBase
    {

        private readonly ISearchApiRepository _searchApiRepository;

        public SearchApiController(ISearchApiRepository searchApiRepository)
        {
            _searchApiRepository = searchApiRepository;
        }

        [HttpPost("study")]
        public async Task<List<StudyDto>> GetStudySearchResults(SearchApiQueryDto searchApiQueryDto)
        {
            return await _searchApiRepository.GetStudySearchResults(searchApiQueryDto);
        }
        
        [HttpPost("object")]
        public async Task<List<StudyDto>> GetObjectSearchResults(SearchApiQueryDto searchApiQueryDto)
        {
            return await _searchApiRepository.GetObjectSearchResults(searchApiQueryDto);
        }
    }
}