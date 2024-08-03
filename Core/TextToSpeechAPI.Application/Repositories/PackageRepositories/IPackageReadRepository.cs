using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechAPI.Domain.Entities;

namespace TextToSpeechAPI.Application.Repositories.PackageRepositories
{
    public interface IPackageReadRepository:IReadRepository<Package>
    {
    }
}
