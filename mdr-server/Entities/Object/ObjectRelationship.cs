using Nest;

namespace mdr_server.Entities.Object
{
    public class ObjectRelationship
    {
        [Number(Name = "id")]
        public int Id { get; set; }
        
        [Text(Name = "relationship_type")]
        public string RelationshipType { get; set; }
        
        [Number(Name = "target_object_id")]
        public int TargetObjectId { get; set; }
    }
}