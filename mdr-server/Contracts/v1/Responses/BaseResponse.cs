using System.Collections.Generic;


namespace mdr_server.Contracts.v1.Responses
{
    public class BaseResponse
    {
        public long Total { get; set; } 
        public ICollection<StudyListResponse.StudyListResponse> Data { get; set; }
    }
}