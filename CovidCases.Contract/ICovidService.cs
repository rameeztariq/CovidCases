using CovidCases.Contract.ViewModels;
using System.Threading.Tasks;

namespace CovidCases.Contract
{
    public interface ICovidService
    {
        Task<ResponseViewModel> GetCovidSummary();
        Task<ResponseViewModel> GetUAEHistory();
    }
}
