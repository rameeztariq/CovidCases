using CovidCases.Contract;
using CovidCases.Contract.ViewModels;
using CovidCases.Data.Models;
using CovidCases.Data.Repositories;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Dynamic;

namespace CovidCases.Service
{
    public class UsersService : Repository<Users>, IUsersService
    {
        public UsersService(ApplicationDBContext repositoryPatternDemoContext) : base(repositoryPatternDemoContext)
        {
        }

        public async Task<ResponseViewModel> AddUserAsync(UserInputViewModel viewModel)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                var checkUser = await GetAsync(o => o.Email == viewModel.Email);
                if (checkUser.Count() < 1)
                {
                    Users user = new Users()
                    {
                        Email = viewModel.Email,
                        Password = viewModel.Password,
                        FirstName = viewModel.FirstName,
                        LastName = viewModel.LastName
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

        public async Task<ResponseViewModel> DeleteUserAsync(int Id)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                var user = await GetAsync(o => o.Id == Id);
                if (user != null)
                {
                    // Delete User
                    var IsSuccess = await DeleteAsync(user.First());
                    if(IsSuccess)
                    {
                        response.StatusCode = 200;
                        response.Message = "User has been deleted.";
                    }
                    else
                    {
                        response.StatusCode = 400;
                        response.Message = "Unable to delete the user.";
                    }
                    response.Succeeded = IsSuccess;
                    return response;
                }
            }
            catch(Exception ex)
            {
                response.StatusCode = 400;
                response.Succeeded = false;
                response.Message = "Error Occured.";
                return response;
            }
            throw new NotImplementedException();
        }

        public async Task<ResponseViewModel> LoginUserAsync(UserLoginViewModel viewModel)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                var user = await GetAsync(o => o.Email == viewModel.Email && o.Password==viewModel.Password);
                if (user!= null)
                {
                    response.StatusCode = 200;
                    response.Succeeded = true;
                    dynamic objUser = new ExpandoObject();
                    objUser.Email = user.First().Email.ToString();
                    objUser.Id = user.First().Id.ToString();
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
                response.Message = "Error Occured.";
                return response;
            }
            throw new NotImplementedException();
        }

        public async Task<ResponseViewModel> UpdateUserAsync(int Id,UsersViewModel viewModel)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                var user = await GetAsync(o => o.Id == Id);
                if (user != null)
                {
                    user.First().FirstName = viewModel.FirstName;
                    user.First().LastName = viewModel.LastName;
                    var result = await UpdateAsync(user.First());
                    response.StatusCode = 201;
                    response.Succeeded = true;
                    response.Message = "User has been updated.";
                    return response;
                }
                else
                {
                    response.StatusCode = 400;
                    response.Succeeded = false;
                    response.Message = "No user exists.";
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                response.Succeeded = false;
                response.Message = "Error Occured.";
                return response;
            }
        }
    }
}
