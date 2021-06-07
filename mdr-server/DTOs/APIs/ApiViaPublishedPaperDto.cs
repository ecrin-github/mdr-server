namespace mdr_server.DTOs.APIs
{
    public class ApiViaPublishedPaperDto : ApiBaseQueryDto
    {
        public string SearchType { get; set; }
        public string SearchValue { get; set; }
    }
}