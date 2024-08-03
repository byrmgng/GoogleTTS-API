using Microsoft.Extensions.DependencyInjection;
using TextToSpeechAPI.Application.Abstractions.Services;
using TextToSpeechAPI.Application.Abstractions.Tokens;
using TextToSpeechAPI.Infrastructure.Services;
using TextToSpeechAPI.Infrastructure.Services.Tokens;

namespace TextToSpeechAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection collection)
        {
            collection.AddScoped<IUserTokenHandler, UserTokenHandler>();
            collection.AddScoped<IGttsService, GttsService>();
        }
    }
}
