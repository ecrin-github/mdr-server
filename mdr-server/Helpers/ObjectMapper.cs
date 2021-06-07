using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.DTOs.Study;
using mdr_server.Entities.Object;
using mdr_server.Entities.Study;
using mdr_server.Interfaces;

namespace mdr_server.Helpers
{
    public class ObjectMapper : IObjectMapper
    {
        private readonly IStudyRepository _studyRepository;
        private readonly IStudyMapper _studyMapper;

        public ObjectMapper(IStudyRepository studyRepository, IStudyMapper studyMapper)
        {
            _studyRepository = studyRepository;
            _studyMapper = studyMapper;
        }
        public async Task<List<StudyDto>> MapObjects(List<Object> objects)
        {
            List<StudyDto> studies = new List<StudyDto>();

            foreach (var obj in objects)
            {
                List<Study> fetchedStudies = await _studyRepository.GetFetchedStudies(obj.LinkedStudies);
                List<StudyDto> mappedStudies = await _studyMapper.MapStudies(fetchedStudies);
                foreach (var study in mappedStudies)
                {
                    studies.Add(study);
                }
            }

            return studies;
        }
    }
}