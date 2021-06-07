namespace mdr_server.DTOs.Queries
{
    public class ElasticQueryDto
    {
        public string Index { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public object ElasticQuery { get; set; }
    }
}