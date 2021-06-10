using System.Collections.Generic;
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
        public async Task<List<StudyListResponse>> GetStudySearchResults(RawQueryRequest rawQueryRequest)
        {
            return await _rawQueryRepository.GetStudySearchResults(rawQueryRequest);
        }
        
        [HttpPost(ApiRoutes.RawQuery.GetObjectSearchResults)]
        public async Task<List<StudyListResponse>> GetObjectSearchResults(RawQueryRequest rawQueryRequest)
        {
            return await _rawQueryRepository.GetObjectSearchResults(rawQueryRequest);
        }
    }
}