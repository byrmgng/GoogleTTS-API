using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeechAPI.Application.DTOs.UserDTOs
{
    public class LoginUserDTO
    {
        public TokenDTO Token { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }

    }
}
