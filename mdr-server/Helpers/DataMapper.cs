using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.DTOs.Study;
using mdr_server.Entities.Object;
using mdr_server.Entities.Study;
using mdr_server.Interfaces;

namespace mdr_server.Helpers
{
    public class DataMapper : IDataMapper
    {
        private readonly IStudyRepository _studyRepository;
        private readonly IObjectRepository _objectRepository;

        public DataMapper(IStudyRepository studyRepository, IObjectRepository objectRepository)
        {
            _studyRepository = studyRepository;
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

        public async Task<List<StudyDto>> MapObjects(List<Object> objects)
        {
            List<StudyDto> studies = new List<StudyDto>();

            foreach (var obj in objects)
            {
                List<Study> fetchedStudies = await _studyRepository.GetFetchedStudies(obj.LinkedStudies);
                List<StudyDto> mappedStudies = await MapStudies(fetchedStudies);
                foreach (var study in mappedStudies)
                {
                    studies.Add(study);
                }
            }

            return studies;
        }
    }
}