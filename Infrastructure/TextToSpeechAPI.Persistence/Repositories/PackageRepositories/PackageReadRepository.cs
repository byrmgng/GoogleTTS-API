using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechAPI.Application.Repositories.PackageRepositories;
using TextToSpeechAPI.Domain.Entities;
using TextToSpeechAPI.Persistence.Contexts;

namespace TextToSpeechAPI.Persistence.Repositories.PackageRepositories
{
    public class PackageReadRepository : ReadRepository<Package>, IPackageReadRepository
    {
        public PackageReadRepository(TextToSpeechAPIDbContext context) : base(context)
        {
        }
    }
}
