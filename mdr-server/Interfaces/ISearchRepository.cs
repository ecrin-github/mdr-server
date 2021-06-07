using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.DTOs.Queries;
using mdr_server.DTOs.Study;

namespace mdr_server.Interfaces
{
    public interface ISearchRepository
    {
        Task<List<StudyDto>> GetStudySearchResults(SearchQueryDto searchQueryDto);
        Task<List<StudyDto>> GetObjectSearchResults(SearchQueryDto searchQueryDto);
    }
}