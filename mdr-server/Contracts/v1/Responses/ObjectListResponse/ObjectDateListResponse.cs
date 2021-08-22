using mdr_server.Entities.Object;

namespace mdr_server.Contracts.v1.Responses.ObjectListResponse
{
    public class ObjectDateListResponse
    {
        #nullable enable
        public int? Id { get; set; }
        
        public string? DateType { get; set; }
        
        public bool? DateIsRange { get; set; }
        
        public string? DateAsString { get; set; }
        
        public Date? StartDate { get; set; }

        public Date? EndDate { get; set; }
        
        public string? Comments { get; set; }
    }
}