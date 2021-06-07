using Nest;

namespace mdr_server.Entities.Study
{
    public class MinAge
    {
        [Number(Name = "value")]
        public int? Value { get; set; }
        
        [Text(Name = "unit_name")]
        public string UnitName { get; set; }
    }
}