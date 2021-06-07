using Nest;

namespace mdr_server.Entities.Study
{
    public class StudyFeature
    {
        [Number(Name = "id")]
        public int? Id { get; set; }
        
        [Text(Name = "feature_type")]
        public string FeatureType { get; set; }
        
        [Text(Name = "feature_value")]
        public string FeatureValue { get; set; }
    }
}