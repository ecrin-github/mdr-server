using Nest;

namespace mdr_server.Entities.Study
{
    public class StudyTitle
    {
        [Number(Name = "id")]
        public int Id { get; set; }
        
        [Text(Name = "title_type")]
        public string TitleType { get; set; }
        
        [Text(Name = "title_text")]
        public string TitleText { get; set; }
        
        [Text(Name = "lang_code")]
        public string LangCode { get; set; }
        
        [Text(Name = "comments")]
        public string Comments { get; set; }
    }
}