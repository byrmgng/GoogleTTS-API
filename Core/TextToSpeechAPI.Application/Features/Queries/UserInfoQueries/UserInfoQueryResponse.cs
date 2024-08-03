using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeechAPI.Application.Features.Queries.UserInfoQueries
{
    public class UserInfoQueryResponse
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string PackageName { get; set; }
        public string RemainingCharacters { get; set; }
        public string RenewalDate { get; set; }
    }
}
