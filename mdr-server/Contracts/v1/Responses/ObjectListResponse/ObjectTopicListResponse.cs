namespace mdr_server.Contracts.v1.Responses.ObjectListResponse
{
    public class ObjectTopicListResponse
    {
        #nullable enable
        public int? Id { get; set; }
        
        public string? TopicType { get; set; }
        
        public bool? MeshCoded { get; set; }
        
        public string? MeshCode { get; set; }
        
        public string? MeshValue { get; set; }

        public string? OriginalValue { get; set; }
    }
}