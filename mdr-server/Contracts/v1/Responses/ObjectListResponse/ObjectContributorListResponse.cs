using mdr_server.Entities.Object;

namespace mdr_server.Contracts.v1.Responses.ObjectListResponse
{
    public class ObjectContributorListResponse
    {
        public int Id { get; set; }
        
        public string ContributionType { get; set; }
        
        public bool IsIndividual { get; set; }
        
        public ContribOrg Organisation { get; set; }
        
        public Person Person { get; set; }
    }
}