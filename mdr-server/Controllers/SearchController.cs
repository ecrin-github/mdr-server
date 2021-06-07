using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.DTOs.Queries;
using mdr_server.DTOs.Study;
using mdr_server.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace mdr_server.Controllers
{
    [ApiController]
    [Route("search")]
    public class SearchController : ControllerBase
    {

        private readonly ISearchRepository _searchRepository;

        public SearchController(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        [HttpPost("study")]
        public async Task<List<StudyDto>> GetStudySearchResults(SearchQueryDto searchQueryDto)
        {
            return await _searchRepository.GetStudySearchResults(searchQueryDto);
        }
        
        [HttpPost("object")]
        public async Task<List<StudyDto>> GetObjectSearchResults(SearchQueryDto searchQueryDto)
        {
            return await _searchRepository.GetObjectSearchResults(searchQueryDto);
        }
    }
}