using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextToSpeechAPI.Application.Features.Commands.AppUserCommands.CreateAppUserCommands;
using TextToSpeechAPI.Application.Features.Commands.AppUserCommands.LoginAppUserCommands;
using TextToSpeechAPI.Application.Features.Commands.AppUserCommands.RefreshTokenLoginCommands;
using TextToSpeechAPI.Application.Features.Queries.UserInfoQueries;

namespace TextToSpeechAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser(CreateAppUserCommandRequest createAppUserCommandRequest)
        {
            CreateAppUserCommandResponse createAppUserCommandResponse = await _mediator.Send(createAppUserCommandRequest);
            return Ok(createAppUserCommandResponse);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> LoginUser(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse loginUserCommandResponse = await _mediator.Send(loginUserCommandRequest);
            return Ok(loginUserCommandResponse);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLoginCustomer(RefreshTokenLoginRequest refreshTokenLoginRequest)
        {
            RefreshTokenLoginResponse refreshTokenLoginResponse = await _mediator.Send(refreshTokenLoginRequest);
            return Ok(refreshTokenLoginResponse);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserInfo()
        {
            var username = User.Identity.Name;
            UserInfoQueryResponse userInfoQueryResponse = null;
            if (username != null)
            {
                UserInfoQueryRequest userInfoQueryRequest = new UserInfoQueryRequest() { Username = username };
                userInfoQueryResponse = await _mediator.Send(userInfoQueryRequest);
            }
            return Ok(userInfoQueryResponse);
        }



    }
}
