using Nest;

namespace mdr_server.Entities.Object
{
    public class ContribOrg
    {
        [Number(Name = "id")]
        public int Id { get; set; }
        
        [Text(Name = "name")]
        public string Name { get; set; }
        
        [Text(Name = "ror_id")]
        public string RorId { get; set; }
    }
}