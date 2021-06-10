using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.Contracts.v1.Requests.Query;
using mdr_server.Contracts.v1.Responses;

namespace mdr_server.Interfaces
{
    public interface IRawQueryRepository
    {
        Task<List<StudyListResponse>> GetStudySearchResults(RawQueryRequest rawQueryRequest);
        Task<List<StudyListResponse>> GetObjectSearchResults(RawQueryRequest rawQueryRequest);
    }
}