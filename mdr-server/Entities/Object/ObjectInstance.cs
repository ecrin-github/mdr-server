using Nest;

namespace mdr_server.Entities.Object
{
    public class ObjectInstance
    {
        [Number(Name = "id")]
        public int Id { get; set; }
        
        [Text(Name = "repository_org")]
        public string RepositoryOrg { get; set; }
        
        [Object]
        [PropertyName("access_details")]
        public InstanceAccessDetails AccessDetails { get; set; }
        
        [Object]
        [PropertyName("resource_details")]
        public InstanceResourceDetails ResourceDetails { get; set; }
    }
}