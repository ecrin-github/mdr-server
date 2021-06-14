using System.Collections.Generic;
using Nest;

namespace mdr_server.Entities.Object
{
    public class Object
    {
        [Number(Name = "id")]
        public int? Id { get; set; }
        
        [Text(Name = "doi")]
        public string Doi { get; set; }
        
        [Text(Name = "display_title")]
        public string DisplayTitle { get; set; }
        
        [Text(Name = "version")]
        public string Version { get; set; }
        
        [Text(Name = "object_class")]
        public string ObjectClass { get; set; }
        
        [Text(Name = "object_type")]
        public string ObjectType { get; set; }
        
        [Date(Name = "publication_year", Format = "YYYY")]
        public int? PublicationYear { get; set; }
        
        [Text(Name = "lang_code")]
        public string LangCode { get; set; }
        
        [Text(Name = "managing_organisation")]
        public string ManagingOrganisation { get; set; }
        
        [Text(Name = "access_type")]
        public string AccessType { get; set; }
        
        [Object]
        [PropertyName("access_details")]
        public AccessDetails AccessDetails { get; set; }
        
        [Number(Name = "eosc_category")]
        public int? EoscCategory { get; set; } 
        
        [Object]
        [PropertyName("dataset_record_keys")]
        public DatasetRecordKeys DatasetRecordKeys { get; set; }
        
        [Object]
        [PropertyName("dataset_deident_level")]
        public DatasetDeidentLevel DatasetDeidentLevel { get; set; }
        
        [Object]
        [PropertyName("dataset_consent")]
        public DatasetConsent DatasetConsent { get; set; }
        
        [Text(Name = "object_url")]
        public string ObjectUrl { get; set; }
        
        [Nested]
        [PropertyName("object_instances")]
        public ICollection<ObjectInstance> ObjectInstances { get; set; }
        
        [Nested]
        [PropertyName("object_titles")]
        public ICollection<ObjectTitle> ObjectTitles { get; set; }
        
        [Nested]
        [PropertyName("object_dates")]
        public ICollection<ObjectDate> ObjectDates { get; set; }
        
        [Nested]
        [PropertyName("object_contributors")]
        public ICollection<ObjectContributor> ObjectContributors { get; set; }
        
        [Nested]
        [PropertyName("object_topics")]
        public ICollection<ObjectTopic> ObjectTopics { get; set; }
        
        [Nested]
        [PropertyName("object_identifiers")]
        public ICollection<ObjectIdentifier> ObjectIdentifiers { get; set; }
        
        [Nested]
        [PropertyName("object_descriptions")]
        public ICollection<ObjectDescription> ObjectDescriptions { get; set; }
        
        [Nested]
        [PropertyName("object_rights")]
        public ICollection<ObjectRight> ObjectRights { get; set; }
        
        [Nested]
        [PropertyName("object_relationships")]
        public ICollection<ObjectRelationship> ObjectRelationships { get; set; }
        
        [Number(Name = "linked_studies")]
        public int[] LinkedStudies { get; set; }
        
        [Text(Name = "provenance_string")]
        public string ProvenanceString { get; set; }
    }
}