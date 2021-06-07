using Nest;

namespace mdr_server.Entities.Object
{
    public class ObjectDescription
    {
        [Number(Name = "id")]
        public int? Id { get; set; }
        
        [Text(Name = "description_type")]
        public string DescriptionType { get; set; }
        
        [Text(Name = "description_label")]
        public string DescriptionLabel { get; set; }
        
        [Text(Name = "description_text")]
        public string DescriptionText { get; set; }
        
        [Text(Name = "lang_code")]
        public string LangCode { get; set; }
    }
}