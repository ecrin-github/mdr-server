using Nest;

namespace mdr_server.Entities.Study
{
    public class StudyTopic
    {
        [Number(Name = "id")]
        public int? Id { get; set; }
        
        [Text(Name = "topic_type")]
        public string TopicType { get; set; }
        
        [Boolean(Name = "mesh_coded")]
        public bool MeshCoded { get; set; }
        
        [Text(Name = "topic_code")]
        public string TopicCode { get; set; }
        
        [Text(Name = "topic_value")]
        public string TopicValue { get; set; }
        
        [Text(Name = "topic_qualcode")]
        public string TopicQualCode { get; set; }
        
        [Text(Name = "topic_qualvalue")]
        public string TopicQualValue { get; set; }
        
        [Text(Name = "original_value")]
        public string OriginalValue { get; set; }
    }
}