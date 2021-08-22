namespace mdr_server.Contracts.v1.Responses.StudyListResponse
{
    public class StudyRelationListResponse
    {
        #nullable enable
        public int? Id { get; set; }
        
        public string? RelationshipType { get; set; }
        
        public int? TargetStudyId { get; set; }
    }
}