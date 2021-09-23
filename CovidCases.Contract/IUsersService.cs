using CovidCases.Contract.ViewModels;
using CovidCases.Data.Models;
using CovidCases.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CovidCases.Contract
{
    public interface IUsersService : IRepository<Users>
    {
        Task<ResponseViewModel> AddUserAsync(UsersViewModel newUser);
        Task<ResponseViewModel> LoginUserAsync(UserLoginViewModel viewModel);
    }
}
