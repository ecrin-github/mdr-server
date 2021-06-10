using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.Entities.Object;
using mdr_server.Entities.Study;

namespace mdr_server.Interfaces
{
    public interface IFetchedDataRepository
    {
        Task<List<Study>> GetFetchedStudies(int[] ids);
        Task<List<Object>> GetFetchedObjects(int[] ids);
    }
}