using System.Collections.Generic;

namespace mdr_server.Entities.FetchedData
{
    public class FetchedStudies
    {
        public long Total { get; set; }
        public List<Study.Study> Studies { get; set; }
    }
}