using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.DAL.Interfaces;
using TeachToEach.Domain.Entity;
using TeachToEach.Domain.Response;
using TeachToEach.Domain.ViewModels.User;
using TeachToEach.Service.Interfaces;

namespace TeachToEach.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<IBaseResponse<bool>> CreateUser(UserViewModel newUserModel)
        {
            var response = new BaseResponse<bool>();
            try
            {
                User newUser = new User()
                {
                    first_name = newUserModel.first_name,
                    last_name = newUserModel.last_name,
                    email = newUserModel.email,
                    age = newUserModel.age
                };
                bool operationResult = await userRepository.Create(newUser);
                if (operationResult)
                {
                    response.StatusCode = Domain.Enum.StatusCode.OK;
                    response.Description = "Пользователь добавлен";
                }
                else
                {
                    response.StatusCode = Domain.Enum.StatusCode.UserNotCreated;
                    response.Description = "Пользователь не добавлен";
                }
                response.Data = operationResult;

            }catch (Exception ex)
            {
                response.Description = $"[CreateUser] : {ex.Message}";
                response.StatusCode = Domain.Enum.StatusCode.InternalServerError;
                response.Data = false;
            }
            return response;
        }

        public async Task<IBaseResponse<bool>> DeleteUser(int id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var userRequest = await GetUser(id);
                if(userRequest.Data == null)
                {
                    response.StatusCode = Domain.Enum.StatusCode.UserNotFound;
                    response.Description = "Пользователь не найден";
                    response.Data = false;
                    return response;
                }

                bool operationResult = await userRepository.Delete(userRequest.Data);
                if (operationResult)
                {
                    response.StatusCode = Domain.Enum.StatusCode.OK;
                    response.Description = "Пользователь удалён";
                }
                else
                {
                    response.StatusCode = Domain.Enum.StatusCode.UserNotRemoved;
                    response.Description = "Пользователь не удалён";
                }
                response.Data = operationResult;

            }
            catch (Exception ex)
            {
                response.Description = $"[DeleteUser] : {ex.Message}";
                response.StatusCode = Domain.Enum.StatusCode.InternalServerError;
                response.Data = false;
            }
            return response;
        }

        public async Task<IBaseResponse<User>> GetUser(int id)
        {
            var response = new BaseResponse<User>();
            try
            {
                var user = await userRepository.Get(id);
                if (user == null)
                {
                    response.StatusCode = Domain.Enum.StatusCode.UserNotFound;
                    response.Description = "Пользователь не найден";
                }
                else
                {
                    response.StatusCode = Domain.Enum.StatusCode.OK;
                    response.Description = "Пользователь найден";
                    response.Data = user;
                }
                
            }
            catch (Exception ex)
            {
                response.Description = $"[GetUser] : {ex.Message}";
                response.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }
            return response;
        }

        public async Task<IBaseResponse<User>> GetUserByFirstName(string first_name)
        {
            var response = new BaseResponse<User>();
            try
            {
                var user = await userRepository.GetByFirstName(first_name);
                if (user == null)
                {
                    response.StatusCode = Domain.Enum.StatusCode.UserNotFound;
                    response.Description = "Пользователь не найден";
                }
                else
                {
                    response.StatusCode = Domain.Enum.StatusCode.OK;
                    response.Description = "Пользователь найден";
                    response.Data = user;
                }

            }
            catch (Exception ex)
            {
                response.Description = $"[GetUserByFirstName] : {ex.Message}";
                response.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }
            return response;
        }

        public async Task<IBaseResponse<IEnumerable<User>>> GetUsers()
        {
            var responce = new BaseResponse<IEnumerable<User>>();
            try
            {
                var users = await userRepository.Select();

                responce.Description = $"Найдено {users.Count()} элементов";
                responce.StatusCode = Domain.Enum.StatusCode.OK;
                responce.Data = users;

                return responce;

            }catch (Exception ex)
            {
                responce.Description = $"[GetUsers] : {ex.Message}";
                responce.StatusCode = Domain.Enum.StatusCode.InternalServerError;

                return responce;
            }
        }
    }
}
