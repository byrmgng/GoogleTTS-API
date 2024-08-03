using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechAPI.Application.Abstractions.Services;
using TextToSpeechAPI.Application.Abstractions.Tokens;
using TextToSpeechAPI.Application.DTOs.UserDTOs;
using TextToSpeechAPI.Application.Repositories.PackageUsageRepositories;
using TextToSpeechAPI.Application.ViewModels.User;
using TextToSpeechAPI.Domain.Entities;
using TextToSpeechAPI.Domain.Entities.Identities;
using TextToSpeechAPI.Persistence.Repositories.PackageUsageRepositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TextToSpeechAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        readonly IPackageUsageWriteRepository _packageUsageWriteRepository;
        readonly SignInManager<AppUser> _signInManager;
        readonly IUserTokenHandler _userTokenHandler;
        readonly IConfiguration _configuration;

        public UserService( IConfiguration configuration,UserManager<AppUser> userManager, IUserTokenHandler userTokenHandler,IPackageUsageWriteRepository packageUsageWriteRepository, SignInManager<AppUser> signInManager)
        {
            _packageUsageWriteRepository = packageUsageWriteRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _userTokenHandler = userTokenHandler;
            _configuration = configuration;

        }
        public async Task<CreateUserDTO> CreateUserAsync(VM_Create_User vmCreateUser)
        {
            PackageUsage packageUsage = new()
            {
                ID = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                PackageID = new Guid("382c74c3-721d-4f34-80e5-57657b6cbc27"),
                RenewalDate = DateTime.Today.AddMonths(1),
                RemainingCharacters = 10000,
            };
            _packageUsageWriteRepository.AddAsync(packageUsage);
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                NameSurname = vmCreateUser.NameSurname,
                Email = vmCreateUser.Email,
                UserName = vmCreateUser.Email,
                PackageUsageID = packageUsage.ID,
            }, vmCreateUser.Password);
            if(result.Succeeded)
            {
                _packageUsageWriteRepository.SaveAsync();

                return new()
                {
                    Success = true,
                    Message = "Kullanıcı Kaydı Başarılı",
                };
            }
            return new()
            {
                Success = false,
                Message = result.ToString(),
            };
            
        }

        public async Task<GetUserInfoDTO> GetUserInfoAsync(string username)
        {
            var userInfo = await _userManager.Users
                .Include(x => x.PackageUsage)
                .ThenInclude(x => x.Package)
                .Where(x => x.UserName == username)
                .Select(x => new GetUserInfoDTO
                {
                    NameSurname = x.NameSurname.ToString(),
                    Email = x.Email.ToString(),
                    PackageName = x.PackageUsage.Package.Name.ToString(),
                    RenewalDate = x.PackageUsage.RenewalDate.ToString(),
                    RemainingCharacters = x.PackageUsage.RemainingCharacters.ToString(),
                }).FirstOrDefaultAsync();
            return userInfo;
        }

        public async Task<LoginUserDTO> LoginUserAsync(VM_Login_User vmLoginUser)
        {
            LoginUserDTO loginUserDTO = new();

            AppUser? user = await _userManager.FindByEmailAsync(vmLoginUser.MailorPhoneNumber);
            if (user == null)
                user = await _userManager.FindByNameAsync(vmLoginUser.MailorPhoneNumber);//Username telefon numarasına eşit
            if (user == null)
            {
                loginUserDTO.Succeeded = false;
                loginUserDTO.Message = "Kullanıcı bulunamadı";
            }
            else
            {
                SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, vmLoginUser.Password, false);
                loginUserDTO.Succeeded = result.Succeeded;
                if (result.Succeeded)
                {
                    loginUserDTO.Message = "Hoşgeldin : " + user.NameSurname;
                    loginUserDTO.Token = _userTokenHandler.CreateAccessToken(user);
                    user.RefleshToken = loginUserDTO.Token.RefreshToken;
                    user.RefleshTokenEndDate = loginUserDTO.Token.Expiration.AddMinutes(Convert.ToInt32(_configuration["UserToken:LifeTimeMinute"]));
                    await _userManager.UpdateAsync(user);
                }
                else
                {
                    loginUserDTO.Message = "Şifre Hatalı";
                }
            }
            return loginUserDTO;
        }

        public async Task<LoginUserDTO> RefreshTokenLoginUserAsync(string token)
        {
            LoginUserDTO loginUserDTO = new();
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefleshToken == token);
            if (user != null && user?.RefleshTokenEndDate > DateTime.UtcNow)
            {
                loginUserDTO.Token = _userTokenHandler.CreateAccessToken(user);
                user.RefleshToken = loginUserDTO.Token.RefreshToken;
                user.RefleshTokenEndDate = loginUserDTO.Token.Expiration.AddMinutes(Convert.ToInt32(_configuration["UserToken:LifeTimeMinute"]));
                await _userManager.UpdateAsync(user);
                loginUserDTO.Succeeded = true;
            }
            return loginUserDTO;
        }
    }
}
