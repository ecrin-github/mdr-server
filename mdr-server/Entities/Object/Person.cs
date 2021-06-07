using Nest;

namespace mdr_server.Entities.Object
{
    public class Person
    {
        [Text(Name = "family_name")]
        public string FamilyName { get; set; }
        
        [Text(Name = "given_name")]
        public string GivenName { get; set; }
        
        [Text(Name = "full_name")]
        public string FullName { get; set; }
        
        [Text(Name = "orcid")]
        public string Orcid { get; set; }
        
        [Text(Name = "affiliation")]
        public string Affiliation { get; set; }
    }
}