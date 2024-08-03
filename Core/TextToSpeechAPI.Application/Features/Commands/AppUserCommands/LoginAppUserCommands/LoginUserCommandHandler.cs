using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechAPI.Application.Abstractions.Services;
using TextToSpeechAPI.Application.DTOs.UserDTOs;

namespace TextToSpeechAPI.Application.Features.Commands.AppUserCommands.LoginAppUserCommands
{
    public class LoginUserCommandHandler:IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        public readonly IUserService _userService;

        public LoginUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            LoginUserDTO result = await _userService.LoginUserAsync(new()
            {
                MailorPhoneNumber = request.MailorPhoneNumber,
                Password = request.Password,
            });
            return new()
            {
                Success = result.Succeeded,
                Message = result.Message,
                Token = result.Token,
            };
        }
    }
}
