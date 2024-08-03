using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeechAPI.Application.Features.Commands.AppUserCommands.CreateAppUserCommands
{
    public class CreateAppUserCommandResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
