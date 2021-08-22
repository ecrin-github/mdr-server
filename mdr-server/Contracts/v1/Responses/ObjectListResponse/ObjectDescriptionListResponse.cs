namespace mdr_server.Contracts.v1.Responses.ObjectListResponse
{
    public class ObjectDescriptionListResponse
    {
        #nullable enable
        public int? Id { get; set; }
        
        public string? DescriptionType { get; set; }
        
        public string? DescriptionLabel { get; set; }
        
        public string? DescriptionText { get; set; }
        
        public string? LangCode { get; set; }
    }
}