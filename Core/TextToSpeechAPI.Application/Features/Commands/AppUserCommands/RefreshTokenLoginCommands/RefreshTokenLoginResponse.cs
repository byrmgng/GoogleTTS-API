using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechAPI.Application.DTOs;

namespace TextToSpeechAPI.Application.Features.Commands.AppUserCommands.RefreshTokenLoginCommands
{
    public class RefreshTokenLoginResponse
    {
        public TokenDTO Token { get; set; }
        public bool Success { get; set; }
    }
}
