using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.Entities.Object;

namespace mdr_server.Interfaces
{
    public interface IObjectRepository
    {
        Task<List<Object>> GetFetchedObjects(int[] ids);
    }
}