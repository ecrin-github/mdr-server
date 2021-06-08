using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.DTOs.Queries;
using mdr_server.DTOs.Study;

namespace mdr_server.Interfaces
{
    public interface ISearchApiRepository
    {
        Task<List<StudyDto>> GetStudySearchResults(SearchApiQueryDto searchApiQueryDto);
        Task<List<StudyDto>> GetObjectSearchResults(SearchApiQueryDto searchApiQueryDto);
    }
}