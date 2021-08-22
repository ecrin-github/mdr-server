namespace mdr_server.Contracts.v1.Responses.ObjectListResponse
{
    public class ObjectRightListResponse
    {
        #nullable enable
        public int? Id { get; set; }
        
        public string? RightsName { get; set; }
        
        public string? RightsUrl { get; set; }
        
        public string? Comments { get; set; }
    }
}