namespace mdr_server.Contracts.v1.Responses.StudyListResponse
{
    public class StudyTitleListResponse
    {
        public int Id { get; set; }
        
        public string TitleType { get; set; }
        
        public string TitleText { get; set; }
        
        public string LangCode { get; set; }
        
        public string Comments { get; set; }
    }
}