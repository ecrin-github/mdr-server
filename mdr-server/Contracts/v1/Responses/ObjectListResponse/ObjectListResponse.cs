using System.Collections.Generic;
using mdr_server.Entities.Object;

namespace mdr_server.Contracts.v1.Responses.ObjectListResponse
{
    public class ObjectListResponse
    {
        #nullable enable
        public int? Id { get; set; }
        
        public string? Doi { get; set; }
        
        public string? DisplayTitle { get; set; }
        
        public string? Version { get; set; }
        
        public string? ObjectClass { get; set; }
        
        public string? ObjectType { get; set; }
        
        public string? ObjectUrl { get; set; }
        
        public int? PublicationYear { get; set; }
        
        public string? LangCode { get; set; }
        
        public ManagingOrg? ManagingOrganisation { get; set; }
        
        public string? AccessType { get; set; }
        
        public AccessDetails? AccessDetails { get; set; }
        
        public int? EoscCategory { get; set; } 
        
        public DatasetRecordKeys? DatasetRecordKeys { get; set; }
        
        public DatasetDeidentLevel? DatasetDeidentLevel { get; set; }
        
        public DatasetConsent? DatasetConsent { get; set; }
        
        public ICollection<ObjectInstanceListResponse>? ObjectInstances { get; set; }
        
        public ICollection<ObjectTitleListResponse>? ObjectTitles { get; set; }
        
        public ICollection<ObjectDateListResponse>? ObjectDates { get; set; }
        
        public ICollection<ObjectContributorListResponse>? ObjectContributors { get; set; }
        
        public ICollection<ObjectTopicListResponse>? ObjectTopics { get; set; }
        
        public ICollection<ObjectIdentifierListResponse>? ObjectIdentifiers { get; set; }
        
        public ICollection<ObjectDescriptionListResponse>? ObjectDescriptions { get; set; }
        
        public ICollection<ObjectRightListResponse>? ObjectRights { get; set; }
        
        public ICollection<ObjectRelationshipListResponse>? ObjectRelationships { get; set; }
        
        public int[]? LinkedStudies { get; set; }
        
        public string? ProvenanceString { get; set; }
    }
}