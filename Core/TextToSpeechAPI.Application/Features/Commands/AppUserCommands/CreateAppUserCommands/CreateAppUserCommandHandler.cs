using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechAPI.Application.Abstractions.Services;
using TextToSpeechAPI.Application.DTOs.UserDTOs;

namespace TextToSpeechAPI.Application.Features.Commands.AppUserCommands.CreateAppUserCommands
{
    public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommandRequest, CreateAppUserCommandResponse>
    {
        private readonly IUserService _userService;

        public CreateAppUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateAppUserCommandResponse> Handle(CreateAppUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserDTO createUserDTO = await _userService.CreateUserAsync(new()
            {
                Email = request.Email,
                NameSurname = request.NameSurname,
                Password = request.Password,
            });
            return new()
            {
                Message = createUserDTO.Message,
                Success = createUserDTO.Success,
            };
        }   
    }
}
