using Nest;

namespace mdr_server.Entities.Object
{
    public class Date
    {
        [Number(Name = "year")]
        #nullable enable
        public int? Year { get; set; }
        
        [Number(Name = "month")]
        #nullable enable
        public int? Month { get; set; }
        
        [Number(Name = "day")]
        #nullable enable
        public int? Day { get; set; }
    }
}