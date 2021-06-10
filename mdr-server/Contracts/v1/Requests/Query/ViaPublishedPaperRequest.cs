namespace mdr_server.Contracts.v1.Requests.Query
{
    public class ViaPublishedPaperRequest : BaseQueryRequest
    {
        public string SearchType { get; set; }
        public string SearchValue { get; set; }
    }
}