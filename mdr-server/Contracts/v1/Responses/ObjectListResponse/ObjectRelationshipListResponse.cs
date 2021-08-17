namespace mdr_server.Contracts.v1.Responses.ObjectListResponse
{
    public class ObjectRelationshipListResponse
    {
        public int Id { get; set; }
        
        public string RelationshipType { get; set; }
        
        public int TargetObjectId { get; set; }
    }
}