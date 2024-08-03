using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeechAPI.Application.Features.Commands.AppUserCommands.LoginAppUserCommands
{
    public class LoginUserCommandRequest:IRequest<LoginUserCommandResponse>
    {
        public string MailorPhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
