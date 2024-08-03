using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechAPI.Application.DTOs.UserDTOs;
using TextToSpeechAPI.Application.ViewModels.User;

namespace TextToSpeechAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserDTO> CreateUserAsync(VM_Create_User vmCreateUser);
        Task<LoginUserDTO> LoginUserAsync(VM_Login_User vmLoginUser);
        Task<LoginUserDTO> RefreshTokenLoginUserAsync(string token);
        Task<GetUserInfoDTO> GetUserInfoAsync(string username);
    }
}
