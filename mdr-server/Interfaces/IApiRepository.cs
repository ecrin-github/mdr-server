using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.DTOs.APIs;
using mdr_server.DTOs.Study;

namespace mdr_server.Interfaces
{
    public interface IApiRepository
    {
        Task<List<StudyDto>> SpecificStudyApi(ApiSpecificStudyDto apiSpecificStudyDto);
        Task<List<StudyDto>> ByStudyCharacteristicsApi(ApiStudyCharacteristicsDto apiStudyCharacteristicsDto);
        Task<List<StudyDto>> ViaPublishedPaperApi(ApiViaPublishedPaperDto apiViaPublishedPaperDto);
        Task<List<StudyDto>> ByStudyIdApi(ApiStudyIdDto apiStudyIdDto);
    }
}