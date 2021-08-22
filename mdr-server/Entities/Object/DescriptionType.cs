using Nest;

namespace mdr_server.Entities.Object
{
    public class DescriptionType
    {
        [Number(Name = "id")]
        #nullable enable
        public int? Id { get; set; }
        
        [Text(Name = "name")]
        #nullable enable
        public string? Name { get; set; }
    }
}