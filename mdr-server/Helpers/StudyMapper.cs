using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.DTOs.Study;
using mdr_server.Entities.Study;
using mdr_server.Interfaces;

namespace mdr_server.Helpers
{
    public class StudyMapper : IStudyMapper
    {

        private readonly IObjectRepository _objectRepository;
        
        public StudyMapper(IObjectRepository objectRepository)
        {
            _objectRepository = objectRepository;
        }
        
        public async Task<List<StudyDto>> MapStudies(List<Study> studies)
        {
            List<StudyDto> studiesDto = new List<StudyDto>();

            foreach (var study in studies)
            {
                var studyDto = new StudyDto
                {
                    Id = study.Id,
                    DisplayTitle = study.DisplayTitle,
                    BriefDescription = study.BriefDescription,
                    StudyType = study.StudyType,
                    StudyStatus = study.StudyStatus,
                    StudyGenderElig = study.StudyGenderElig,
                    StudyEnrolment = study.StudyEnrolment,
                    MinAge = study.MinAge,
                    MaxAge = study.MaxAge,
                    StudyIdentifiers = study.StudyIdentifiers,
                    StudyTitles = study.StudyTitles,
                    StudyFeatures = study.StudyFeatures,
                    StudyTopics = study.StudyTopics,
                    StudyRelationships = study.StudyRelationships,
                    ProvenanceString = study.ProvenanceString,
                    LinkedDataObjects = await _objectRepository.GetFetchedObjects(study.LinkedDataObjects)
                };
                studiesDto.Add(studyDto);
            }

            return studiesDto;
        }
    }
}