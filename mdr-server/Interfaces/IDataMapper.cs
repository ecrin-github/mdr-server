using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.DTOs.Study;
using mdr_server.Entities.Object;
using mdr_server.Entities.Study;

namespace mdr_server.Interfaces
{
    public interface IDataMapper
    {
        Task<List<StudyDto>> MapStudies(List<Study> studies);
        Task<List<StudyDto>> MapObjects(List<Object> objects);
    }
}