using System.Collections.Generic;
using System.Threading.Tasks;
using mdr_server.Contracts.v1.Responses;
using mdr_server.Entities.Object;
using mdr_server.Entities.Study;
using mdr_server.Interfaces;

namespace mdr_server.Helpers
{
    public class DataMapper : IDataMapper
    {
        private readonly IFetchedDataRepository _fetchedDataRepository;

        public DataMapper(IFetchedDataRepository fetchedDataRepository)
        {
            _fetchedDataRepository = fetchedDataRepository;
        }
        
        public async Task<List<StudyListResponse>> MapStudies(List<Study> studies)
        {
            List<StudyListResponse> studiesDto = new List<StudyListResponse>();

            foreach (var study in studies)
            {
                var studyDto = new StudyListResponse
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
                    LinkedDataObjects = await _fetchedDataRepository.GetFetchedObjects(study.LinkedDataObjects)
                };
                studiesDto.Add(studyDto);
            }

            return studiesDto;
        }

        public async Task<List<StudyListResponse>> MapObjects(List<Object> objects)
        {
            List<StudyListResponse> studies = new List<StudyListResponse>();

            foreach (var obj in objects)
            {
                List<Study> fetchedStudies = await _fetchedDataRepository.GetFetchedStudies(obj.LinkedStudies);
                List<StudyListResponse> mappedStudies = await MapStudies(fetchedStudies);
                foreach (var study in mappedStudies)
                {
                    studies.Add(study);
                }
            }

            return studies;
        }
    }
}