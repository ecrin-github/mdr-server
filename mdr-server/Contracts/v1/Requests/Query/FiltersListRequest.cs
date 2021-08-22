using System;
using System.Collections.Generic;

namespace mdr_server.Contracts.v1.Requests.Query
{
    public class FiltersListRequest
    {
        public ICollection<object> StudyFilters { get; set; }
        
        public ICollection<object> ObjectFilters { get; set; }
    }
}