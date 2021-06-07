using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.DTOs.Study;
using mdr_server.Entities.Object;

namespace mdr_server.Interfaces
{
    public interface IObjectMapper
    {
        Task<List<StudyDto>> MapObjects(List<Object> objects);
    }
}