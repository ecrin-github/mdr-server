using mdr_server.Entities.Object;

namespace mdr_server.Contracts.v1.Responses.ObjectListResponse
{
    public class ObjectInstanceListResponse
    {
        #nullable enable
        public int? Id { get; set; }
        
        public string? RepositoryOrg { get; set; }
        
        public InstanceAccessDetails? AccessDetails { get; set; }
        
        public InstanceResourceDetails? ResourceDetails { get; set; }
    }
}