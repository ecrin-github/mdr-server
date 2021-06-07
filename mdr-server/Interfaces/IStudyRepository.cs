using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.Entities.Study;

namespace mdr_server.Interfaces
{
    public interface IStudyRepository
    {
        Task<List<Study>> GetFetchedStudies(int[] ids);
    }
}