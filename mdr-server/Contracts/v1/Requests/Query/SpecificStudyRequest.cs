namespace mdr_server.Contracts.v1.Requests.Query
{
    public class SpecificStudyRequest : BaseQueryRequest
    {
        public int SearchType { get; set; }
        public string SearchValue { get; set; }
    }
}