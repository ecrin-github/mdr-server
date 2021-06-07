namespace mdr_server.DTOs.APIs
{
    public class ApiSpecificStudyDto : ApiBaseQueryDto
    {
        public int SearchType { get; set; }
        public string SearchValue { get; set; }
    }
}