using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechAPI.Application.DTOs;

namespace TextToSpeechAPI.Application.Features.Commands.AppUserCommands.LoginAppUserCommands
{
    public class LoginUserCommandResponse
    {
        public TokenDTO Token { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
