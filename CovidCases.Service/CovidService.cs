using CovidCases.Contract;
using CovidCases.Contract.ViewModels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CovidCases.Service
{
    public class CovidService : ICovidService
    {   
        /// <summary>
        /// Get Covid Summary from the API
        /// </summary>
        public async Task<ResponseViewModel> GetCovidSummary()
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                var result = await GetResponse("https://api.covid19api.com/summary");
                response.StatusCode = 200;
                response.Succeeded = true;
                SummaryViewModel summary = JsonConvert.DeserializeObject<SummaryViewModel>(result);
                response.Payload = summary;
                return response;
            }
            catch
            {
                response.StatusCode = 400;
                response.Succeeded = false;
                response.Message = "Error Occured.";
                return response;
            }
        }

        /// <summary>
        /// Get UAE History from the covid API
        /// </summary>
        public async Task<ResponseViewModel> GetUAEHistory()
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                var result = await GetResponse("https://api.covid19api.com/dayone/country/united-arab-emirates");
                response.StatusCode = 200;
                response.Succeeded = true;               
                List<UAEHistorViewModel> history = JsonConvert.DeserializeObject<List<UAEHistorViewModel>>(result);
                response.Payload = history;
                return response;
            }
            catch(Exception ex)
            {
                response.StatusCode = 400;
                response.Succeeded = false;
                response.Message = ex.Message.ToString();
                return response;
            }
        }
       
        /// <summary>
        /// Returns API response
        /// </summary>
        private async Task<string> GetResponse(string url)
        {
            try
            {
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse restResponse = await client.ExecuteAsync(request);
                return restResponse.Content;
            }
            catch { return "Error Occured."; }
        }

    }

}
