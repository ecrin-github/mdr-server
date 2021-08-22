namespace mdr_server.Contracts.v1.Responses.StudyListResponse
{
    public class StudyFeatureListResponse
    {
        #nullable enable
        public int? Id { get; set; }
        
        public string? FeatureType { get; set; }
        
        public string? FeatureValue { get; set; }
    }
}