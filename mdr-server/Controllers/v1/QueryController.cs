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
    public class QueryController : ControllerBase
    {
        
        private readonly IQueryRepository _queryRepository;
        public QueryController(IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        [HttpPost(ApiRoutes.Query.GetSpecificStudy)]
        public async Task<List<StudyListResponse>> GetSpecificStudy(SpecificStudyRequest specificStudyRequest)
        {
            return await _queryRepository.GetSpecificStudy(specificStudyRequest);
        }
        
        [HttpPost(ApiRoutes.Query.GetByStudyCharacteristics)]
        public async Task<List<StudyListResponse>> GetByStudyCharacteristics(StudyCharacteristicsRequest studyCharacteristicsRequest)
        {
            return await _queryRepository.GetByStudyCharacteristics(studyCharacteristicsRequest);
        }
        
        [HttpPost(ApiRoutes.Query.GetViaPublishedPaper)]
        public async Task<List<StudyListResponse>> GetViaPublishedPaper(ViaPublishedPaperRequest viaPublishedPaperRequest)
        {
            return await _queryRepository.GetViaPublishedPaper(viaPublishedPaperRequest);
        }
        
        [HttpPost(ApiRoutes.Query.GetByStudyId)]
        public async Task<List<StudyListResponse>> GetByStudyId(StudyIdRequest studyIdRequest)
        {
            return await _queryRepository.GetByStudyId(studyIdRequest);
        }
    }
}