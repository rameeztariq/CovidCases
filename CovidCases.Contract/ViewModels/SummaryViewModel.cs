using System;
using System.Collections.Generic;
using System.Text;

namespace CovidCases.Contract.ViewModels
{
    public class Countries
    {
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Slug { get; set; }
        public string NewConfirmed { get; set; }
        public string TotalConfirmed { get; set; }
        public string NewDeaths { get; set; }
        public string TotalDeaths { get; set; }
        public string NewRecovered { get; set; }
        public string TotalRecovered { get; set; }
        public string Date { get; set; }

    }
    public class SummaryViewModel
    {
        public Dictionary<string, object> Global { get; set; }
        public Countries[] Countries { get; set; }
        public DateTime Date { get; set; }
    }
}
