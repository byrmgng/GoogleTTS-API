using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechAPI.Application.Abstractions.Services;
using TextToSpeechAPI.Application.DTOs.UserDTOs;

namespace TextToSpeechAPI.Application.Features.Commands.AppUserCommands.RefreshTokenLoginCommands
{
    public class RefreshTokenLoginHandler:IRequestHandler<RefreshTokenLoginRequest, RefreshTokenLoginResponse>
    {
        private readonly IUserService _userService;

        public RefreshTokenLoginHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<RefreshTokenLoginResponse> Handle(RefreshTokenLoginRequest request, CancellationToken cancellationToken)
        {
            LoginUserDTO result = await _userService.RefreshTokenLoginUserAsync(request.RefreshToken);
            return new()
            {
                Success = result.Succeeded,
                Token = result.Token,
            };
        }
    }
}
