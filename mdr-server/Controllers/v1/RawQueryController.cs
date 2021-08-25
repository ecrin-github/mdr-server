using System.Threading.Tasks;
using mdr_server.Contracts.v1;
using mdr_server.Contracts.v1.Requests.Query;
using mdr_server.Contracts.v1.Responses;
using mdr_server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace mdr_server.Controllers.v1
{
    [ApiController]
    public class RawQueryController : ControllerBase
    {
        private readonly IRawQueryRepository _rawQueryRepository;
        
        public RawQueryController(IRawQueryRepository rawQueryRepository)
        {
            _rawQueryRepository = rawQueryRepository;
        }

        [HttpPost(ApiRoutes.RawQuery.GetStudySearchResults)]
        public async Task<ActionResult<SearchResponse>> GetStudySearchResults(RawQueryRequest rawQueryRequest)
        {
            var response = await _rawQueryRepository.GetStudySearchResults(rawQueryRequest);
            return Ok(new SearchResponse()
            {
                Total = response.Total,
                Page = rawQueryRequest.Page,
                Size = rawQueryRequest.Size,
                Data = response.Data
            });
        }
        
        [HttpPost(ApiRoutes.RawQuery.GetObjectSearchResults)]
        public async Task<ActionResult<SearchResponse>> GetObjectSearchResults(RawQueryRequest rawQueryRequest)
        {
            var response = await _rawQueryRepository.GetObjectSearchResults(rawQueryRequest);
            return Ok(new SearchResponse()
            {
                Total = response.Total,
                Page = rawQueryRequest.Page,
                Size = rawQueryRequest.Size,
                Data = response.Data
            });
        }
    }
}