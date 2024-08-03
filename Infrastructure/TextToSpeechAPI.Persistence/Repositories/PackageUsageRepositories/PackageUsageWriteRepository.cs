using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechAPI.Application.Repositories.PackageUsageRepositories;
using TextToSpeechAPI.Domain.Entities;
using TextToSpeechAPI.Persistence.Contexts;

namespace TextToSpeechAPI.Persistence.Repositories.PackageUsageRepositories
{
    public class PackageUsageWriteRepository : WriteRepository<PackageUsage>, IPackageUsageWriteRepository
    {
        public PackageUsageWriteRepository(TextToSpeechAPIDbContext context) : base(context)
        {
        }
    }
}
