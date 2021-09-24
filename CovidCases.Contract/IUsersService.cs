using CovidCases.Contract.ViewModels;
using CovidCases.Data.Models;
using CovidCases.Data.Repositories;
using System.Threading.Tasks;

namespace CovidCases.Contract
{
    public interface IUsersService : IRepository<Users>
    {
        Task<ResponseViewModel> AddUserAsync(UserInputViewModel viewModel);
        Task<ResponseViewModel> UpdateUserAsync(int Id,UsersViewModel viewModel);
        Task<ResponseViewModel> LoginUserAsync(UserLoginViewModel viewModel);
        Task<ResponseViewModel> DeleteUserAsync(int Id);

    }
}
