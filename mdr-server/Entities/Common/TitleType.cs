using Nest;

namespace mdr_server.Entities.Common
{
    public class TitleType
    {
        [Number(Name = "id")]
        public int Id { get; set; }
        
        [Text(Name = "name")]
        public string Name { get; set; }
    }
}