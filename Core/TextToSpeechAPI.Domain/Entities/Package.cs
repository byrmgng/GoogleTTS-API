using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechAPI.Domain.Entities.Commons;

namespace TextToSpeechAPI.Domain.Entities
{
    public class Package:BaseEntity
    {
        public string Name { get; set; }
        public int TotalCharacters { get; set; }
        public float Price { get; set; }
        public ICollection<PackageUsage> PackageUsages { get; set; }
    }
}
