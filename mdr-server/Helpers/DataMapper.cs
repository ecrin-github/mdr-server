using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mdr_server.Contracts.v1.Requests.Query;
using mdr_server.Contracts.v1.Responses.ObjectListResponse;
using mdr_server.Contracts.v1.Responses.StudyListResponse;
using mdr_server.Entities.FetchedData;
using mdr_server.Entities.Object;
using mdr_server.Entities.Study;
using mdr_server.Interfaces;
using Object = mdr_server.Entities.Object.Object;


namespace mdr_server.Helpers
{
    public class DataMapper : IDataMapper
    {
        private readonly IFetchedDataRepository _fetchedDataRepository;

        public DataMapper(IFetchedDataRepository fetchedDataRepository)
        {
            _fetchedDataRepository = fetchedDataRepository;
        }

        private ICollection<StudyIdentifierListResponse> StudyIdentifierListResponseMapper(ICollection<StudyIdentifier> studyIdentifiers)
        {
            if (studyIdentifiers == null) return null;
            if (studyIdentifiers.Count <= 0) return null;
            ICollection<StudyIdentifierListResponse> studyIdentifierListResponses =
                new List<StudyIdentifierListResponse>();
            
            foreach (var studyIdentifier in studyIdentifiers)
            {
                var studyIdentifierObject = new StudyIdentifierListResponse
                {
                    Id = studyIdentifier.Id,
                    IdentifierValue = studyIdentifier.IdentifierValue,
                    IdentifierType = studyIdentifier.IdentifierType!.Name,
                    IdentifierDate = studyIdentifier.IdentifierDate,
                    IdentifierLink = studyIdentifier.IdentifierLink,
                    IdentifierOrg = studyIdentifier.IdentifierOrg
                };
                
                studyIdentifierListResponses.Add(studyIdentifierObject);
            }

            return studyIdentifierListResponses;

        }


        private ICollection<StudyTitleListResponse> StudyTitleListResponseMapper(ICollection<StudyTitle> studyTitles)
        {
            if (studyTitles == null) return null;
            if (studyTitles.Count <= 0) return null;
            List<StudyTitleListResponse> studyTitleListResponses =
                new List<StudyTitleListResponse>();

            foreach (var studyTitle in studyTitles)
            {
                var studyTitleObject = new StudyTitleListResponse
                {
                    Id = studyTitle.Id,
                    TitleType = studyTitle.TitleType!.Name,
                    TitleText = studyTitle.TitleText,
                    LangCode = studyTitle.LangCode,
                    Comments = studyTitle.Comments
                };
                studyTitleListResponses.Add(studyTitleObject);
            }

            return studyTitleListResponses;
        }


        private ICollection<StudyFeatureListResponse> StudyFeatureListResponseMapper(ICollection<StudyFeature> studyFeatures)
        {
            if (studyFeatures == null) return null;
            if (studyFeatures.Count <= 0) return null;

            List<StudyFeatureListResponse> studyFeatureListResponses = new List<StudyFeatureListResponse>();

            foreach (var studyFeature in studyFeatures)
            {
                var studyFeatureObject = new StudyFeatureListResponse
                {
                    Id = studyFeature.Id,
                    FeatureType = studyFeature.FeatureType!.Name,
                    FeatureValue = studyFeature.FeatureValue!.Name
                };
                studyFeatureListResponses.Add(studyFeatureObject);
            }

            return studyFeatureListResponses;
        }

        private ICollection<StudyTopicListResponse> StudyTopicResponseMapper(ICollection<StudyTopic> studyTopics)
        {
            if (studyTopics == null) return null;
            if (studyTopics.Count <= 0) return null;
            
            List<StudyTopicListResponse> studyTopicListResponses = new List<StudyTopicListResponse>();

            foreach (var studyTopic in studyTopics)
            {
                var studyTopicObject = new StudyTopicListResponse
                {
                    Id = studyTopic.Id,
                    TopicType = studyTopic.TopicType!.Name,
                    MeshCoded = studyTopic.MeshCoded,
                    MeshCode = studyTopic.MeshCode,
                    MeshValue = studyTopic.MeshValue,
                    OriginalValue = studyTopic.OriginalValue
                };
                studyTopicListResponses.Add(studyTopicObject);
            }
            
            return studyTopicListResponses;
        }


        private ICollection<StudyRelationListResponse> StudyRelationListResponseMapper(
            ICollection<StudyRelation> studyRelations)
        {
            if (studyRelations == null) return null;
            if (studyRelations.Count <= 0) return null;
            
            List<StudyRelationListResponse> studyRelationListResponses = new List<StudyRelationListResponse>();

            foreach (var studyRelation in studyRelations)
            {
                var studyRelationObject = new StudyRelationListResponse
                {
                    Id = studyRelation.Id,
                    RelationshipType = studyRelation.RelationshipType!.Name,
                    TargetStudyId = studyRelation.TargetStudyId
                };
                studyRelationListResponses.Add(studyRelationObject);
            }
            
            return studyRelationListResponses;
        }


        private string ObjectUrlExtraction(ICollection<ObjectInstance> objectInstances)
        {
            string objectUrlString = null;

            if (objectInstances == null) return null;
            if (objectInstances.Count <= 0) return null;
            if (objectInstances.Count > 0)
            {
                if (!string.IsNullOrEmpty(objectInstances.First().AccessDetails!.Url))
                {
                    objectUrlString = objectInstances.First().AccessDetails!.Url;
                }
            }
            
            return objectUrlString;
        }


        private ICollection<ObjectContributorListResponse> ObjectContributorListResponseMapper(ICollection<ObjectContributor> objectContributors)
        {
            if (objectContributors == null) return null;
            if (objectContributors.Count <= 0) return null;
            List<ObjectContributorListResponse> objectContributorListResponses =
                new List<ObjectContributorListResponse>();

            foreach (var objectContributor in objectContributors)
            {
                var objectContributorRecord = new ObjectContributorListResponse
                {
                    Id = objectContributor.Id,
                    ContributionType = objectContributor.ContributionType!.Name,
                    IsIndividual = objectContributor.IsIndividual,
                    Organisation = objectContributor.Organisation,
                    Person = objectContributor.Person
                };
                objectContributorListResponses.Add(objectContributorRecord);
            }

            return objectContributorListResponses;
        }
        
        
        private ICollection<ObjectDateListResponse> ObjectDateListResponseMapper(ICollection<ObjectDate> objectDates)
        {
            if (objectDates == null) return null;
            if (objectDates.Count <= 0) return null;
            List<ObjectDateListResponse> objectDateListResponses =
                new List<ObjectDateListResponse>();

            foreach (var objectDate in objectDates)
            {
                var objectDateRecord = new ObjectDateListResponse
                {
                    Id = objectDate.Id,
                    DateType = objectDate.DateType!.Name,
                    DateIsRange = objectDate.DateIsRange,
                    DateAsString = objectDate.DateAsString,
                    StartDate = objectDate.StartDate,
                    EndDate = objectDate.EndDate,
                    Comments = objectDate.Comments
                };
                objectDateListResponses.Add(objectDateRecord);
            }

            return objectDateListResponses;
        }
        
        
        private ICollection<ObjectDescriptionListResponse> ObjectDescriptionListResponseMapper(ICollection<ObjectDescription> objectDescriptions)
        {
            if (objectDescriptions == null) return null;
            if (objectDescriptions.Count <= 0) return null;
            List<ObjectDescriptionListResponse> objectDescriptionListResponses =
                new List<ObjectDescriptionListResponse>();

            foreach (var objectDescription in objectDescriptions)
            {
                var objectDescriptionRecord = new ObjectDescriptionListResponse
                {
                    Id = objectDescription.Id,
                    DescriptionType = objectDescription.DescriptionType!.Name,
                    DescriptionLabel = objectDescription.DescriptionLabel,
                    DescriptionText = objectDescription.DescriptionText,
                    LangCode = objectDescription.LangCode
                };
                objectDescriptionListResponses.Add(objectDescriptionRecord);
            }
            
            return objectDescriptionListResponses;
        }
        
        
        private ICollection<ObjectIdentifierListResponse> ObjectIdentifierListResponseMapper(ICollection<ObjectIdentifier> objectIdentifiers)
        {
            if (objectIdentifiers == null) return null;
            if (objectIdentifiers.Count <= 0) return null;
            List<ObjectIdentifierListResponse> objectIdentifierListResponses =
                new List<ObjectIdentifierListResponse>();

            foreach (var objectIdentifier in objectIdentifiers)
            {
                var objectIdentifierRecord = new ObjectIdentifierListResponse
                {
                    Id = objectIdentifier.Id,
                    IdentifierValue = objectIdentifier.IdentifierValue,
                    IdentifierType = objectIdentifier.IdentifierType!.Name,
                    IdentifierDate = objectIdentifier.IdentifierDate,
                    IdentifierOrg = objectIdentifier.IdentifierOrg
                };
                objectIdentifierListResponses.Add(objectIdentifierRecord);
            }

            return objectIdentifierListResponses;
        }
        
        
        private ICollection<ObjectInstanceListResponse> ObjectInstanceListResponseMapper(ICollection<ObjectInstance> objectInstances)
        {
            if (objectInstances == null) return null;
            if (objectInstances.Count <= 0) return null;
            List<ObjectInstanceListResponse> objectInstanceListResponses =
                new List<ObjectInstanceListResponse>();

            foreach (var objectInstance in objectInstances)
            {
                var objectInstanceRecord = new ObjectInstanceListResponse
                {
                    Id = objectInstance.Id,
                    RepositoryOrg = objectInstance.RepositoryOrg!.Name,
                    AccessDetails = objectInstance.AccessDetails,
                    ResourceDetails = objectInstance.ResourceDetails
                };
                objectInstanceListResponses.Add(objectInstanceRecord);
            }

            return objectInstanceListResponses;
        }
        
        
        private ICollection<ObjectRelationshipListResponse> ObjectRelationshipListResponseMapper(ICollection<ObjectRelationship> objectRelationships)
        {
            if (objectRelationships == null) return null;
            if (objectRelationships.Count <= 0) return null;
            List<ObjectRelationshipListResponse> objectRelationshipListResponses =
                new List<ObjectRelationshipListResponse>();

            foreach (var objectRelation in objectRelationships)
            {
                var objectRelationRecord = new ObjectRelationshipListResponse
                {
                    Id = objectRelation.Id,
                    RelationshipType = objectRelation.RelationshipType!.Name,
                    TargetObjectId = objectRelation.TargetObjectId
                };
                objectRelationshipListResponses.Add(objectRelationRecord);
            }
            
            return objectRelationshipListResponses;
        }
        
        
        private ICollection<ObjectRightListResponse> ObjectRightListResponseMapper(ICollection<ObjectRight> objectRights)
        {
            if (objectRights == null) return null;
            if (objectRights.Count <= 0) return null;
            List<ObjectRightListResponse> objectRightListResponses =
                new List<ObjectRightListResponse>();

            foreach (var objectRight in objectRights)
            {
                var objectRightRecord = new ObjectRightListResponse
                {
                    Id = objectRight.Id,
                    RightsName = objectRight.RightsName,
                    RightsUrl = objectRight.RightsUrl,
                    Comments = objectRight.Comments
                };
                objectRightListResponses.Add(objectRightRecord);
            }

            return objectRightListResponses;
        }
        
        
        private ICollection<ObjectTitleListResponse> ObjectTitleListResponseMapper(ICollection<ObjectTitle> objectTitles)
        {
            if (objectTitles == null) return null;
            if (objectTitles.Count <= 0) return null;
            List<ObjectTitleListResponse> objectTitleListResponses =
                new List<ObjectTitleListResponse>();

            foreach (var objectTitle in objectTitles)
            {
                var objectTitleRecord = new ObjectTitleListResponse
                {
                    Id = objectTitle.Id,
                    TitleType = objectTitle.TitleType!.Name,
                    TitleText = objectTitle.TitleText,
                    LangCode = objectTitle.LangCode,
                    Comments = objectTitle.Comments
                };
                objectTitleListResponses.Add(objectTitleRecord);
            }

            return objectTitleListResponses;
        }
        
        
        private ICollection<ObjectTopicListResponse> ObjectTopicListResponseMapper(ICollection<ObjectTopic> objectTopics)
        {
            if (objectTopics == null) return null;
            if (objectTopics.Count <= 0) return null;
            List<ObjectTopicListResponse> objectTopicListResponses =
                new List<ObjectTopicListResponse>();

            foreach (var objectTopic in objectTopics)
            {
                var objectTopicRecord = new ObjectTopicListResponse
                {
                    Id = objectTopic.Id,
                    TopicType = objectTopic.TopicType!.Name,
                    MeshCoded = objectTopic.MeshCoded,
                    MeshCode = objectTopic.MeshCode,
                    MeshValue = objectTopic.MeshValue,
                    OriginalValue = objectTopic.OriginalValue
                };
                objectTopicListResponses.Add(objectTopicRecord);
            }

            return objectTopicListResponses;
        }


        private ICollection<ObjectListResponse> ObjectListResponseMapper(ICollection<Object> objects)
        {
            if (objects == null) return null;
            if (objects.Count <= 0) return null;
            List<ObjectListResponse> objectListResponses = new List<ObjectListResponse>();

            foreach (var objectRecord in objects)
            {
                var objectDto = new ObjectListResponse
                {
                    Id = objectRecord.Id,
                    Doi = objectRecord.Doi,
                    DisplayTitle = objectRecord.DisplayTitle,
                    Version = objectRecord.Version,
                    ObjectClass = objectRecord.ObjectClass.Name,
                    ObjectType = objectRecord.ObjectType.Name,
                    ObjectUrl = ObjectUrlExtraction(objectRecord.ObjectInstances),
                    PublicationYear = objectRecord.PublicationYear,
                    LangCode = objectRecord.LangCode,
                    ManagingOrganisation = objectRecord.ManagingOrganisation,
                    AccessType = objectRecord.AccessType.Name,
                    AccessDetails = objectRecord.AccessDetails,
                    EoscCategory = objectRecord.EoscCategory,
                    DatasetRecordKeys = objectRecord.DatasetRecordKeys,
                    DatasetConsent = objectRecord.DatasetConsent,
                    DatasetDeidentLevel = objectRecord.DatasetDeidentLevel,
                    ObjectInstances = ObjectInstanceListResponseMapper(objectRecord.ObjectInstances),
                    ObjectTitles = ObjectTitleListResponseMapper(objectRecord.ObjectTitles),
                    ObjectDates = ObjectDateListResponseMapper(objectRecord.ObjectDates),
                    ObjectContributors = ObjectContributorListResponseMapper(objectRecord.ObjectContributors),
                    ObjectTopics = ObjectTopicListResponseMapper(objectRecord.ObjectTopics),
                    ObjectIdentifiers = ObjectIdentifierListResponseMapper(objectRecord.ObjectIdentifiers),
                    ObjectDescriptions = ObjectDescriptionListResponseMapper(objectRecord.ObjectDescriptions),
                    ObjectRights = ObjectRightListResponseMapper(objectRecord.ObjectRights),
                    ObjectRelationships = ObjectRelationshipListResponseMapper(objectRecord.ObjectRelationships),
                    LinkedStudies = objectRecord.LinkedStudies,
                    ProvenanceString = objectRecord.ProvenanceString
                };
                objectListResponses.Add(objectDto);
            }

            return objectListResponses;

        }


        public async Task<List<StudyListResponse>> MapStudies(FetchedStudies fetchedStudies, FiltersListRequest filtersListRequest)
        {
            if (fetchedStudies.Total <= 0) return null;
            if (fetchedStudies.Studies.Count <= 0) return null;
            List<StudyListResponse> studiesDto = new List<StudyListResponse>();

            foreach (var study in fetchedStudies.Studies)
            {
                FetchedObjects fetchedObjects = await _fetchedDataRepository.GetFetchedObjects(study.LinkedDataObjects, filtersListRequest);
                
                MinAgeResponse minAgeResponse = null;
                if (study.MinAge != null)
                {
                    minAgeResponse = new MinAgeResponse
                    {
                        Value = study.MinAge.Value,
                        UnitName = study.MinAge.UnitName
                    };
                }

                MaxAgeResponse maxAgeResponse = null;
                if (study.MaxAge != null)
                {
                    maxAgeResponse = new MaxAgeResponse
                    {
                        Value = study.MaxAge.Value,
                        UnitName = study.MaxAge.UnitName
                    };
                }
            
                var studyDto = new StudyListResponse
                {
                    Id = study.Id!,
                    DisplayTitle = study.DisplayTitle,
                    BriefDescription = study.BriefDescription,
                    StudyType = study.StudyType.Name,
                    StudyStatus = study.StudyStatus.Name,
                    StudyGenderElig = study.StudyGenderElig.Name,
                    StudyEnrolment = study.StudyEnrolment,
                    MinAge = minAgeResponse,
                    MaxAge = maxAgeResponse,
                    StudyIdentifiers = StudyIdentifierListResponseMapper(study.StudyIdentifiers),
                    StudyTitles = StudyTitleListResponseMapper(study.StudyTitles),
                    StudyFeatures = StudyFeatureListResponseMapper(study.StudyFeatures),
                    StudyTopics = StudyTopicResponseMapper(study.StudyTopics),
                    StudyRelationships = StudyRelationListResponseMapper(study.StudyRelationships),
                    ProvenanceString = study.ProvenanceString!,
                    LinkedDataObjects = ObjectListResponseMapper(fetchedObjects.Objects),
                };
                studiesDto.Add(studyDto);
            }
            
            return studiesDto;
        }

        public async Task<List<StudyListResponse>> MapObjects(FetchedObjects fetchedObjects, FiltersListRequest filtersListRequest)
        {
            if (fetchedObjects.Total <= 0) return null;
            if (fetchedObjects.Objects.Count <= 0) return null;
            
            List<StudyListResponse> studies = new List<StudyListResponse>();
            foreach (var obj in fetchedObjects.Objects)
            {
                if (obj.LinkedStudies != null && obj.LinkedStudies.Length > 0)
                {
                    FetchedStudies fetchedStudies = await _fetchedDataRepository.GetFetchedStudies(obj.LinkedStudies, filtersListRequest);

                    if (fetchedStudies.Total > 0)
                    {
                        List<StudyListResponse> mappedStudies = await MapStudies(fetchedStudies, filtersListRequest);
                        foreach (var study in mappedStudies)
                        {
                            studies.Add(study);
                        }
                    }
                }
            }
            return studies;
        }
        
        
        public async Task<List<StudyListResponse>> MapStudy(List<Study> studies)
        {
            List<StudyListResponse> studiesDto = new List<StudyListResponse>();
            foreach (var study in studies)
            {

                FetchedObjects fetchedObjects = await _fetchedDataRepository.GetStudyObjects(study.LinkedDataObjects);
                
                MinAgeResponse minAgeResponse = null;
                if (study.MinAge != null)
                {
                    minAgeResponse = new MinAgeResponse
                    {
                        Value = study.MinAge.Value,
                        UnitName = study.MinAge.UnitName
                    };
                }

                MaxAgeResponse maxAgeResponse = null;
                if (study.MaxAge != null)
                {
                    maxAgeResponse = new MaxAgeResponse
                    {
                        Value = study.MaxAge.Value,
                        UnitName = study.MaxAge.UnitName
                    };
                }
            
                var studyDto = new StudyListResponse
                {
                    Id = study.Id!,
                    DisplayTitle = study.DisplayTitle,
                    BriefDescription = study.BriefDescription,
                    StudyType = study.StudyType.Name,
                    StudyStatus = study.StudyStatus.Name,
                    StudyGenderElig = study.StudyGenderElig.Name,
                    StudyEnrolment = study.StudyEnrolment,
                    MinAge = minAgeResponse,
                    MaxAge = maxAgeResponse,
                    StudyIdentifiers = StudyIdentifierListResponseMapper(study.StudyIdentifiers),
                    StudyTitles = StudyTitleListResponseMapper(study.StudyTitles),
                    StudyFeatures = StudyFeatureListResponseMapper(study.StudyFeatures),
                    StudyTopics = StudyTopicResponseMapper(study.StudyTopics),
                    StudyRelationships = StudyRelationListResponseMapper(study.StudyRelationships),
                    ProvenanceString = study.ProvenanceString!,
                    LinkedDataObjects = ObjectListResponseMapper(fetchedObjects.Objects),
                };
                studiesDto.Add(studyDto);
            }
            
            return studiesDto;
        }


        public async Task<List<StudyListResponse>> MapViaPublishedPaper(List<Object> objects)
        {
            List<StudyListResponse> studies = new List<StudyListResponse>();
            foreach (var obj in objects)
            {
                if (obj.LinkedStudies != null && obj.LinkedStudies.Length > 0)
                {
                    FetchedStudies fetchedStudies = await _fetchedDataRepository.GetObjectStudies(obj.LinkedStudies);

                    if (fetchedStudies.Total > 0)
                    {
                        List<StudyListResponse> mappedStudies = await MapRawStudies(fetchedStudies.Studies);
                        foreach (var study in mappedStudies)
                        {
                            studies.Add(study);
                        }
                    }
                }
            }
            return studies;
        }


        public async Task<List<StudyListResponse>> MapRawStudies(List<Study> studies)
        {
            List<StudyListResponse> studiesDto = new List<StudyListResponse>();
            foreach (var study in studies)
            {
                FetchedObjects fetchedObjects = await _fetchedDataRepository.GetStudyObjects(study.LinkedDataObjects);
                
                MinAgeResponse minAgeResponse = null;
                if (study.MinAge != null)
                {
                    minAgeResponse = new MinAgeResponse
                    {
                        Value = study.MinAge.Value,
                        UnitName = study.MinAge.UnitName
                    };
                }

                MaxAgeResponse maxAgeResponse = null;
                if (study.MaxAge != null)
                {
                    maxAgeResponse = new MaxAgeResponse
                    {
                        Value = study.MaxAge.Value,
                        UnitName = study.MaxAge.UnitName
                    };
                }
            
                var studyDto = new StudyListResponse
                {
                    Id = study.Id!,
                    DisplayTitle = study.DisplayTitle,
                    BriefDescription = study.BriefDescription,
                    StudyType = study.StudyType.Name,
                    StudyStatus = study.StudyStatus.Name,
                    StudyGenderElig = study.StudyGenderElig.Name,
                    StudyEnrolment = study.StudyEnrolment,
                    MinAge = minAgeResponse,
                    MaxAge = maxAgeResponse,
                    StudyIdentifiers = StudyIdentifierListResponseMapper(study.StudyIdentifiers),
                    StudyTitles = StudyTitleListResponseMapper(study.StudyTitles),
                    StudyFeatures = StudyFeatureListResponseMapper(study.StudyFeatures),
                    StudyTopics = StudyTopicResponseMapper(study.StudyTopics),
                    StudyRelationships = StudyRelationListResponseMapper(study.StudyRelationships),
                    ProvenanceString = study.ProvenanceString!,
                    LinkedDataObjects = ObjectListResponseMapper(fetchedObjects.Objects),
                };
                studiesDto.Add(studyDto);
            }
            return studiesDto;
        }
        
        
        public async Task<List<StudyListResponse>> MapRawObjects(List<Object> objects)
        {
            if (objects.Count <= 0) return null;
            List<StudyListResponse> studies = new List<StudyListResponse>();
            foreach (var obj in objects)
            {
                if (obj.LinkedStudies != null && obj.LinkedStudies.Length > 0)
                {
                    FetchedStudies fetchedStudies = await _fetchedDataRepository.GetObjectStudies(obj.LinkedStudies);

                    if (fetchedStudies.Total > 0)
                    {
                        List<StudyListResponse> mappedStudies = await MapRawStudies(fetchedStudies.Studies);
                        foreach (var study in mappedStudies)
                        {
                            studies.Add(study);
                        }
                    }
                }
            }
            return studies;
        }
        
    }
}