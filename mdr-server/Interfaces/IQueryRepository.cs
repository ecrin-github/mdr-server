using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.Contracts.v1.Requests.Query;
using mdr_server.Contracts.v1.Responses;

namespace mdr_server.Interfaces
{
    public interface IQueryRepository
    {
        Task<List<StudyListResponse>> GetSpecificStudy(SpecificStudyRequest specificStudyRequest);
        Task<List<StudyListResponse>> GetByStudyCharacteristics(StudyCharacteristicsRequest studyCharacteristicsRequest);
        Task<List<StudyListResponse>> GetViaPublishedPaper(ViaPublishedPaperRequest viaPublishedPaperRequest);
        Task<List<StudyListResponse>> GetByStudyId(StudyIdRequest studyIdRequest);
    }
}