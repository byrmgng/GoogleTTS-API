using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeechAPI.Application.Features.Commands.AppUserCommands.RefreshTokenLoginCommands
{
    public class RefreshTokenLoginRequest:IRequest<RefreshTokenLoginResponse>
    {
        public string RefreshToken { get; set; }

    }
}
