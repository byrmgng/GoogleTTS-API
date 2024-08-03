
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using TextToSpeechAPI.Application;
using TextToSpeechAPI.Domain.Entities.Identities;
using TextToSpeechAPI.Infrastructure;
using TextToSpeechAPI.Persistence;

namespace TextToSpeechAPI.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices();
            builder.Services.AddInfrastructureServices();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Standart", builder =>
                    builder.WithOrigins("http://localhost:4200", "https://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials());
            });



            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthorization(options =>
            {
                var policy = new AuthorizationPolicyBuilder(IdentityConstants.ApplicationScheme, IdentityConstants.BearerScheme)
                    .RequireAuthenticatedUser()
                    .Build();
                options.DefaultPolicy = policy;
            });
            builder.Services.AddAuthentication().AddJwtBearer("UserToken", options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = true, //Oluþturulacak Tokenin hangi sitelerde kullanýlacaðý belirlenir.
                    ValidateIssuer = true, // Oluþturulacak token deðerini kim oluþturulacak belirlenir.
                    ValidateLifetime = true, // Oluþturulan token deðerinin süresini kontrol etmek için kullanýlýr.
                    ValidateIssuerSigningKey = true, //Üretilecek token deðerinin uygulamaya ait olma durmunu kontrol eder.
                    ValidAudience = builder.Configuration["UserToken:Audience"],
                    ValidIssuer = builder.Configuration["UserToken:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["UserToken:SecurityKey"])),
                    LifetimeValidator = (notbefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
                    NameClaimType = ClaimTypes.Name
                };
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("Standart");

            app.MapControllers();

            app.Run();
        }
    }
}
