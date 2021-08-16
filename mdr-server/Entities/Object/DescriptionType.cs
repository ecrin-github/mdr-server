using Nest;

namespace mdr_server.Entities.Object
{
    public class DescriptionType
    {
        [Number(Name = "id")]
        public int Id { get; set; }
        
        [Text(Name = "name")]
        public int Name { get; set; }
    }
}