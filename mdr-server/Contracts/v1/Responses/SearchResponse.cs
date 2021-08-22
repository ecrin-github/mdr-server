using System.Collections.Generic;

namespace mdr_server.Contracts.v1.Responses
{
    public class SearchResponse : BaseResponse
    {
        public int? Page { get; set; }
        public int? Size { get; set; }
    }
}