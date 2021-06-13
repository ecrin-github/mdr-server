using System.Threading.Tasks;
using mdr_server.Contracts.v1.Requests.Query;
using mdr_server.Contracts.v1.Responses;

namespace mdr_server.Interfaces
{
    public interface IRawQueryRepository
    {
        Task<BaseResponse> GetStudySearchResults(RawQueryRequest rawQueryRequest);
        Task<BaseResponse> GetObjectSearchResults(RawQueryRequest rawQueryRequest);
    }
}