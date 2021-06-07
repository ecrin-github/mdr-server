using Nest;

namespace mdr_server.Entities.Study
{
    public class StudyRelation
    {
        [Number(Name = "id")]
        public int? Id { get; set; }
        
        [Text(Name = "relationship_type")]
        public string RelationshipType { get; set; }
        
        [Number(Name = "target_study_id")]
        public int? TargetStudyId { get; set; }
    }
}