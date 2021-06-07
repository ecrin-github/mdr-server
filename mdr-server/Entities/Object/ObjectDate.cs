using Nest;

namespace mdr_server.Entities.Object
{
    public class ObjectDate
    {
        [Number(Name = "id")]
        public int? Id { get; set; }
        
        [Text(Name = "date_type")]
        public string DateType { get; set; }
        
        [Boolean(Name = "is_date_range")]
        public bool IsDateRange { get; set; }
        
        [Text(Name = "date_as_string")]
        public string DateAsString { get; set; }
        
        [Object]
        [PropertyName("start_date")]
        public Date StartDate { get; set; }
        
        [Object]
        [PropertyName("end_date")]
        public Date EndDate { get; set; }
        
        [Text(Name = "comments")]
        public string Comments { get; set; }
    }
}