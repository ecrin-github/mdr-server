using mdr_server.Entities.Common;
using Nest;

namespace mdr_server.Entities.Object
{
    public class ObjectRelationship
    {
        [Number(Name = "id")]
        #nullable enable
        public int? Id { get; set; }
        
        [Object]
        [PropertyName("relationship_type")]
        #nullable enable
        public RelationType? RelationshipType { get; set; }
        
        [Number(Name = "target_object_id")]
        #nullable enable
        public int? TargetObjectId { get; set; }
    }
}