using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechAPI.Application.Abstractions.Services;
using TextToSpeechAPI.Application.Repositories.PackageRepositories;
using TextToSpeechAPI.Application.Repositories.PackageUsageRepositories;
using TextToSpeechAPI.Domain.Entities.Identities;
using TextToSpeechAPI.Persistence.Contexts;
using TextToSpeechAPI.Persistence.Repositories.PackageRepositories;
using TextToSpeechAPI.Persistence.Repositories.PackageUsageRepositories;
using TextToSpeechAPI.Persistence.Services;

namespace TextToSpeechAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<TextToSpeechAPIDbContext>(options => options.UseSqlServer(Configurations.ConnectionString));


            services.AddScoped<IPackageReadRepository, PackageReadRepository>();
            services.AddScoped<IPackageWriteRepository, PackageWriteRepository>();
            services.AddScoped<IPackageUsageReadRepository, PackageUsageReadRepository>();
            services.AddScoped<IPackageUsageWriteRepository, PackageUsageWriteRepository>();

            services.AddScoped<IUserService, UserService>();


            services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<TextToSpeechAPIDbContext>().AddApiEndpoints();

           
        }
    }
}
