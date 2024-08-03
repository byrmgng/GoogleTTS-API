using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechAPI.Application.Abstractions.Services;
using TextToSpeechAPI.Application.DTOs.UserDTOs;

namespace TextToSpeechAPI.Application.Features.Queries.UserInfoQueries
{
    public class UserInfoQueryHandler : IRequestHandler<UserInfoQueryRequest, UserInfoQueryResponse>
    {
        private readonly IUserService _userService;
        public UserInfoQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserInfoQueryResponse> Handle(UserInfoQueryRequest request, CancellationToken cancellationToken)
        {
            GetUserInfoDTO userInfo = await _userService.GetUserInfoAsync(request.Username);
            return new()
            {
                Email = userInfo.Email,
                NameSurname = userInfo.NameSurname,
                RenewalDate = userInfo.RenewalDate,
                PackageName = userInfo.PackageName,
                RemainingCharacters = userInfo.RemainingCharacters,
            };
        }
    }
}
