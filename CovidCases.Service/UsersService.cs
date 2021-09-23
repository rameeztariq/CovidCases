using CovidCases.Contract;
using CovidCases.Contract.ViewModels;
using CovidCases.Data.Models;
using CovidCases.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Dynamic;

namespace CovidCases.Service
{
    //public class UsersService : IUsersService
    public class UsersService : Repository<Users>, IUsersService
    {
        public UsersService(ApplicationDBContext repositoryPatternDemoContext) : base(repositoryPatternDemoContext)
        {
        }

        public async Task<ResponseViewModel> AddUserAsync(UsersViewModel newUser)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                var checkUser = await GetAsync(o => o.Email == newUser.Email);
                if (checkUser.Count() < 1)
                {
                    Users user = new Users()
                    {
                        Email = newUser.Email,
                        Password = newUser.Password,
                        FirstName = newUser.FirstName,
                        LastName = newUser.LastName
                    };
                    var result = await AddAsync(user);
                    response.StatusCode = 201;
                    response.Succeeded = true;
                    response.Message = "Your record has been saved.";
                    return response;
                }
                else
                {
                    response.StatusCode = 409;
                    response.Succeeded = false;
                    response.Message = "User name already exists.";
                    return response;
                }
               
            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                response.Succeeded = false;
                response.Message = ex.ToString();
                return response;
            }
            throw new NotImplementedException();
        }
        public async Task<ResponseViewModel> LoginUserAsync(UserLoginViewModel viewModel)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                var checkUser = await GetAsync(o => o.Email == viewModel.Email && o.Password==viewModel.Password);
                if (checkUser.Count() == 1)
                {
                    response.StatusCode = 200;
                    response.Succeeded = true;
                    dynamic objUser = new ExpandoObject();
                    objUser.Email = checkUser.First().Email.ToString();
                    objUser.Id = checkUser.First().Id.ToString();
                    response.Payload = objUser;
                    return response;
                }
                else
                {
                    response.StatusCode = 400;
                    response.Succeeded = false;
                    response.Message = "Wrong User Id or Password.";
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                response.Succeeded = false;
                response.Message = ex.ToString();
                return response;
            }
            throw new NotImplementedException();
        }

    }
}
