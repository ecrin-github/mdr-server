using Nest;

namespace mdr_server.Entities.Object
{
    public class DatasetConsent
    {
        [Text(Name = "consent_type")]
        public string ConsentType { get; set; }
        
        [Boolean(Name = "consent_noncommercial")]
        public bool ConsentNoncommercial { get; set; }
        
        [Boolean(Name = "consent_geog_restrict")]
        public bool ConsentGeogRestrict { get; set; }
        
        [Boolean(Name = "consent_research_type")]
        public bool ConsentResearchType { get; set; }
        
        [Boolean(Name = "consent_genetic_only")]
        public bool ConsentGeneticOnly { get; set; }
        
        [Boolean(Name = "consent_no_methods")]
        public bool ConsentNoMethods { get; set; }
        
        [Text(Name = "consents_details")]
        public string ConsentsDetails { get; set; }
    }
}