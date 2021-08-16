using mdr_server.Entities.Common;
using Nest;

namespace mdr_server.Entities.Object
{
    public class ObjectRelationship
    {
        [Number(Name = "id")]
        public int? Id { get; set; }
        
        [Object]
        [PropertyName("relationship_type")]
        public RelationType RelationshipType { get; set; }
        
        [Number(Name = "target_object_id")]
        public int TargetObjectId { get; set; }
    }
}