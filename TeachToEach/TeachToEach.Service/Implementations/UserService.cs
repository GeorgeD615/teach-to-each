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
                var userRequest = await userRepository.Get(id);
                if(userRequest == null)
                {
                    response.StatusCode = Domain.Enum.StatusCode.UserNotFound;
                    response.Description = "Пользователь не найден";
                    response.Data = false;
                    return response;
                }

                bool operationResult = await userRepository.Delete(userRequest);
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
        public async Task<IBaseResponse<bool>> EditUser(int id, UserViewModel userViewModel)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var userRequest = await userRepository.Get(id);
                if(userRequest == null)
                {
                    response.StatusCode = Domain.Enum.StatusCode.UserNotFound;
                    response.Description = "Пользователь не найден";
                    response.Data = false;
                    return response;
                }

                userRequest.first_name = userViewModel.first_name;
                userRequest.last_name = userViewModel.last_name;
                userRequest.email = userViewModel.email;    
                userRequest.age = userViewModel.age;

                bool operationResult = await userRepository.Edit(userRequest);
                if (operationResult)
                {
                    response.Description = "Данные успешно обновлены";
                    response.StatusCode = Domain.Enum.StatusCode.OK;
                }
                else
                {
                    response.Description = "Ошибка: Данные не обновлены";
                    response.StatusCode = Domain.Enum.StatusCode.UserNotUpdated;
                }
                response.Data = operationResult;


            }
            catch(Exception ex)
            {
                response.Description = $"[EditUser] : {ex.Message}";
                response.StatusCode = Domain.Enum.StatusCode.InternalServerError;
                response.Data = false;
            }
            return response;
        }
        public async Task<IBaseResponse<UserViewModel>> GetUser(int id)
        {
            var response = new BaseResponse<UserViewModel>();
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
                    response.Data = new UserViewModel()
                    {
                        first_name = user.first_name,
                        last_name = user.last_name,
                        age = user.age,
                        email = user.email
                    };
                }
                
            }
            catch (Exception ex)
            {
                response.Description = $"[GetUser] : {ex.Message}";
                response.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }
            return response;
        }
        public async Task<IBaseResponse<UserViewModel>> GetUserByFirstName(string first_name)
        {
            var response = new BaseResponse<UserViewModel>();
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
                    response.Data = new UserViewModel() {
                        first_name = user.first_name,
                        last_name = user.last_name,
                        age = user.age,
                        email = user.email
                    };
                }

            }
            catch (Exception ex)
            {
                response.Description = $"[GetUserByFirstName] : {ex.Message}";
                response.StatusCode = Domain.Enum.StatusCode.InternalServerError;
            }
            return response;
        }
        public async Task<IBaseResponse<IEnumerable<UserViewModel>>> GetUsers()
        {
            var responce = new BaseResponse<IEnumerable<UserViewModel>>();
            try
            {
                var users = await userRepository.Select();
                responce.Description = $"Найдено {users.Count()} элементов";
                responce.StatusCode = Domain.Enum.StatusCode.OK;
                responce.Data = users.Select(u => new UserViewModel()
                {
                    first_name = u.first_name,
                    last_name = u.last_name,
                    age = u.age,
                    email = u.email
                });

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
