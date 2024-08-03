using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeechAPI.Domain.Entities.Identities
{
    public class AppUser: IdentityUser
    {
        public string NameSurname { get; set; }
        public Guid? PackageUsageID { get; set; }
        public PackageUsage PackageUsage { get; set; }
        public string? RefleshToken { get; set; }
        public DateTime? RefleshTokenEndDate { get; set; }

    }
}
