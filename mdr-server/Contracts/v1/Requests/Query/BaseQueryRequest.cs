namespace mdr_server.Contracts.v1.Requests.Query
{
    public class BaseQueryRequest
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}