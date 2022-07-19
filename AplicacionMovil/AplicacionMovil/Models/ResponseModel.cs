using System;
using System.Collections.Generic;
using System.Text;

namespace CristianMorocho.Models
{
    public class ResponseModel
    {
        public int status { get; set; }
        public string message { get; set; }
        public string token { get; set; }
    }
}
