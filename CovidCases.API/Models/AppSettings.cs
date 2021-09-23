using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidCases.API.Models
{
    public class AppSettings
    {
        public string Issuer { get; set; }
        public int ExpirationInDays { get; set; }
        public string TokenSecret { get; set; }
        public string ValidIn { get; set; }
    }
}
