using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.Contracts.v1.Requests.Query;
using mdr_server.Contracts.v1.Responses;
using mdr_server.Contracts.v1.Responses.StudyListResponse;


namespace mdr_server.Interfaces
{
    public interface IQueryRepository
    {
        Task<BaseResponse> GetSpecificStudy(SpecificStudyRequest specificStudyRequest);
        Task<BaseResponse> GetByStudyCharacteristics(StudyCharacteristicsRequest studyCharacteristicsRequest);
        Task<BaseResponse> GetViaPublishedPaper(ViaPublishedPaperRequest viaPublishedPaperRequest);
        Task<IEnumerable<StudyListResponse>> GetByStudyId(StudyIdRequest studyIdRequest);
    }
}