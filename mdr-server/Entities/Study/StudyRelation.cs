using mdr_server.Entities.Common;
using Nest;

namespace mdr_server.Entities.Study
{
    public class StudyRelation
    {
        [Number(Name = "id")]
        public int Id { get; set; }
        
        [Object]
        [PropertyName("relationship_type")]
        public RelationType RelationshipType { get; set; }
        
        [Number(Name = "target_study_id")]
        public int TargetStudyId { get; set; }
    }
}