using System.Collections.Generic;
using System;

namespace eVotingApp.Generic
{
    public class ResponseList<T>
    {
        public List<T> Data { get; set; }
        public ResponseError Error { get; set; }
        public bool IsSuccessful { get; set; }
        public string ResponseCode { get; set; }
        public string Description { get; set; }
        public DateTime ResponseTime { get; set; } = DateTime.UtcNow;
    }
}
