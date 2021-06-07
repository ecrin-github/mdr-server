namespace mdr_server.DTOs.Queries
{
    public class SearchQueryDto : BaseQueryDto
    {
        public string Index { get; set; }
        public object ElasticQuery { get; set; }
    }
}