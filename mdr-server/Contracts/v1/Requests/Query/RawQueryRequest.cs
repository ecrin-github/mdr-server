namespace mdr_server.Contracts.v1.Requests.Query
{
    public class RawQueryRequest : BaseQueryRequest
    {
        public object ElasticQuery { get; set; }
    }
}