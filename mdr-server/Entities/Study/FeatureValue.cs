using Nest;

namespace mdr_server.Entities.Study
{
    public class FeatureValue
    {
        [Number(Name = "id")]
        public int Id { get; set; }
        
        [Text(Name = "name")]
        public string Name { get; set; }
    }
}