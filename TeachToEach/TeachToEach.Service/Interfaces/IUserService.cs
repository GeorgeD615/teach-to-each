using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.Domain.Entity;
using TeachToEach.Domain.Response;
using TeachToEach.Domain.ViewModels.User;

namespace TeachToEach.Service.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<IEnumerable<UserViewModel>>> GetUsers();

        Task<IBaseResponse<UserViewModel>> GetUser(int id);

        Task<IBaseResponse<UserViewModel>> GetUserByFirstName(string first_name);

        Task<IBaseResponse<bool>> CreateUser(UserViewModel newUser);

        Task<IBaseResponse<bool>> DeleteUser(int id);

        Task<IBaseResponse<bool>> EditUser(int id, UserViewModel userViewModel);

    }
}
