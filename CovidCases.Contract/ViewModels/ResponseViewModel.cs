using System;
using System.Collections.Generic;
using System.Text;

namespace CovidCases.Contract.ViewModels
{
    public class ResponseViewModel
    {
        public bool Succeeded { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public dynamic Payload { get; set; }
    }
}
