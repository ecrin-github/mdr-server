using System.Threading.Tasks;
using mdr_server.Contracts.v1.Requests.Query;
using mdr_server.Entities.FetchedData;

namespace mdr_server.Interfaces
{
    public interface IFetchedDataRepository
    {
        Task<FetchedStudies> GetFetchedStudies(int[] ids, FiltersListRequest filters);
        Task<FetchedObjects> GetFetchedObjects(int[] ids, FiltersListRequest filters);
        Task<FetchedObjects> GetStudyObjects(int[] ids);
        Task<FetchedStudies> GetObjectStudies(int[] ids);
    }
}