using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechAPI.Domain.Entities.Commons;
using TextToSpeechAPI.Domain.Entities.Identities;

namespace TextToSpeechAPI.Domain.Entities
{
    public class PackageUsage:BaseEntity
    {
        public int RemainingCharacters { get; set; }
        public DateTime? RenewalDate { get; set; }
        public AppUser AppUser { get; set; }
        public Guid? PackageID { get; set; }
        public Package Package { get; set; }
    }
}
