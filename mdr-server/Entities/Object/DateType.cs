using Nest;

namespace mdr_server.Entities.Object
{
    public class DateType
    {
        [Number(Name = "id")]
        public int Id { get; set; }
        
        [Text(Name = "name")]
        public string Name { get; set; }
    }
}