using mdr_server.Entities.Common;
using Nest;

namespace mdr_server.Entities.Study
{
    public class StudyTopic
    {
        [Number(Name = "id")]
        public int Id { get; set; }
        
        [Object]
        [PropertyName("topic_type")]
        public TopicType TopicType { get; set; }
        
        [Boolean(Name = "mesh_coded")]
        public bool MeshCoded { get; set; }
        
        [Text(Name = "mesh_code")]
        public string MeshCode { get; set; }
        
        [Text(Name = "mesh_value")]
        public string MeshValue { get; set; }

        [Text(Name = "original_value")]
        public string OriginalValue { get; set; }
    }
}