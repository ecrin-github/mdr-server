using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.Contracts.v1.Responses;
using mdr_server.Entities.Object;
using mdr_server.Entities.Study;

namespace mdr_server.Interfaces
{
    public interface IDataMapper
    {
        Task<List<StudyListResponse>> MapStudies(List<Study> studies);
        Task<List<StudyListResponse>> MapObjects(List<Object> objects);
    }
}