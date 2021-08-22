namespace mdr_server.Contracts.v1.Requests.Query
{
    public class BaseQueryRequest
    {
        #nullable enable
        public int? Page { get; set; }
        public int? Size { get; set; }
        
        public FiltersListRequest? Filters { get; set; }
    }
}