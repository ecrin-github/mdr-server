using System.Collections.Generic;
using mdr_server.Entities.Study;

namespace mdr_server.Contracts.v1.Responses
{
    public class StudyListResponse
    {
        public int Id { get; set; }
        public string DisplayTitle { get; set; }
        public string BriefDescription { get; set; }
        public string StudyType { get; set; }
        public string StudyStatus { get; set; }
        public string StudyGenderElig { get; set; }
        public string? StudyEnrolment { get; set; }
        public MinAge MinAge { get; set; }
        public MaxAge MaxAge { get; set; }
        public ICollection<StudyIdentifier> StudyIdentifiers { get; set; }
        public ICollection<StudyTitle> StudyTitles { get; set; }
        public ICollection<StudyFeature> StudyFeatures { get; set; }
        public ICollection<StudyTopic> StudyTopics { get; set; }
        public ICollection<StudyRelation> StudyRelationships { get; set; }
        public string ProvenanceString { get; set; }
        public ICollection<Entities.Object.Object> LinkedDataObjects { get; set; }
    }
}