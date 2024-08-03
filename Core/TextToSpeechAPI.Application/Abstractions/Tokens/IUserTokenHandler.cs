using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechAPI.Application.DTOs;
using TextToSpeechAPI.Domain.Entities.Identities;

namespace TextToSpeechAPI.Application.Abstractions.Tokens
{
    public interface IUserTokenHandler
    {
        TokenDTO CreateAccessToken(AppUser appUser);
    }
}
