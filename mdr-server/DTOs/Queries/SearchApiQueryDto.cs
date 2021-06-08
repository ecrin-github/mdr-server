namespace mdr_server.DTOs.Queries
{
    public class SearchApiQueryDto : BaseQueryDto
    {
        public object ElasticQuery { get; set; }
    }
}