using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.DTOs.Study;
using mdr_server.Entities.Study;

namespace mdr_server.Interfaces
{
    public interface IStudyMapper
    {
        Task<List<StudyDto>> MapStudies(List<Study> studies);
    }
}