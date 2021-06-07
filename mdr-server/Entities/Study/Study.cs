using System;
using System.Collections.Generic;
using Nest;

namespace mdr_server.Entities.Study
{
    [ElasticsearchType(RelationName = "study")]
    public class Study
    {
        [Number(Name = "id")]
        public int? Id { get; set; }
        
        [Text(Name = "display_title")]
        public string DisplayTitle { get; set; }
        
        [Text(Name = "brief_description")]
        public string BriefDescription { get; set; }
        
        [Text(Name = "data_sharing_statement")]
        public string DataSharingStatement { get; set; }
        
        [Text(Name = "study_type")]
        public string StudyType { get; set; }
        
        [Text(Name = "study_status")]
        public string StudyStatus { get; set; }
        
        [Text(Name = "study_gender_elig")]
        public string StudyGenderElig { get; set; }
        
        [Text(Name = "study_enrolment")]
        public int? StudyEnrolment { get; set; }
        
        [Object]
        [PropertyName("min_age")]
        public MinAge MinAge { get; set; }
        
        [Object]
        [PropertyName("max_age")]
        public MaxAge MaxAge { get; set; }
        
        [Nested]
        [PropertyName("study_identifiers")]
        public List<StudyIdentifier> StudyIdentifiers { get; set; }
        
        [Nested]
        [PropertyName("study_titles")]
        public List<StudyTitle> StudyTitles { get; set; }
        
        [Nested]
        [PropertyName("study_features")]
        public List<StudyFeature> StudyFeatures { get; set; }
        
        [Nested]
        [PropertyName("study_topics")]
        public List<StudyTopic> StudyTopics { get; set; }
        
        [Nested]
        [PropertyName("study_relationships")]
        public List<StudyRelation> StudyRelationships { get; set; }
        
        [Text(Name="provenance_string")]
        public string ProvenanceString { get; set; }
        
        [Number(Name = "linked_data_objects")]
        public int[] LinkedDataObjects { get; set; }
    }
}