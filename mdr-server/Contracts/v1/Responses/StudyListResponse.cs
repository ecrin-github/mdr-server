using System.Collections.Generic;
using mdr_server.Entities.Study;

namespace mdr_server.Contracts.v1.Responses
{
    public class StudyListResponse
    {
        public int? Id { get; set; }
        public string DisplayTitle { get; set; }
        public string BriefDescription { get; set; }
        public string StudyType { get; set; }
        public string StudyStatus { get; set; }
        public string StudyGenderElig { get; set; }
        public int? StudyEnrolment { get; set; }
        public MinAge MinAge { get; set; }
        public MaxAge MaxAge { get; set; }
        public List<StudyIdentifier> StudyIdentifiers { get; set; }
        public List<StudyTitle> StudyTitles { get; set; }
        public List<StudyFeature> StudyFeatures { get; set; }
        public List<StudyTopic> StudyTopics { get; set; }
        public List<StudyRelation> StudyRelationships { get; set; }
        public string ProvenanceString { get; set; }
        public List<Entities.Object.Object> LinkedDataObjects { get; set; }
    }
}