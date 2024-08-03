using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Google.Cloud.TextToSpeech.V1;
using Microsoft.Extensions.Configuration;
using TextToSpeechAPI.Application.Abstractions.Services;
using TextToSpeechAPI.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using TextToSpeechAPI.Application.DTOs.UserDTOs;
using TextToSpeechAPI.Domain.Entities.Identities;
using Microsoft.EntityFrameworkCore;
using TextToSpeechAPI.Application.Repositories.PackageUsageRepositories;
using TextToSpeechAPI.Domain.Entities;

namespace TextToSpeechAPI.Infrastructure.Services
{
    public class GttsService:IGttsService
    {
        private readonly TextToSpeechClient _client;
        private readonly IConfiguration _configuration;
        private readonly IPackageUsageWriteRepository _packageUsageWriteRepository;
        private readonly IPackageUsageReadRepository _packageUsageReadRepository;
        private readonly UserManager<AppUser> _userManager;


        public GttsService(IConfiguration configuration, 
            IPackageUsageWriteRepository packageUsageWriteRepository, 
            IPackageUsageReadRepository packageUsageReadRepository, 
            UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _packageUsageWriteRepository = packageUsageWriteRepository;
            _packageUsageReadRepository = packageUsageReadRepository;
            _configuration = configuration;

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", _configuration["GoogleTTS:Credential"]);
            _client = TextToSpeechClient.Create();
        }
        public async Task<List<Voice>> GetSupportedVoicesAsync()
        {
            var response = await _client.ListVoicesAsync(new ListVoicesRequest());
            return response.Voices.ToList();
        }
        public async Task<byte[]> SynthesizeSpeechAsync(string username, string text, string languageCode = "en-US", string voiceName = "en-US-Wavenet-D")
        {
            var input = new SynthesisInput
            {
                Text = text
            };

            var voiceSelection = new VoiceSelectionParams
            {
                LanguageCode = languageCode,
                Name = voiceName,
            };

            var audioConfig = new AudioConfig
            {
                AudioEncoding = AudioEncoding.Mp3
            };

            // Ses dosyasını sentezleyin
            var response = await _client.SynthesizeSpeechAsync(input, voiceSelection, audioConfig);

            // ByteString'den byte dizisine dönüştürün
            var audioBytes = response.AudioContent.ToByteArray();
            await UpdateRemainingCharactersAsync(username, text.Length);


            return audioBytes; // Ses dosyasını byte array olarak döndür
        }
        public async Task UpdateRemainingCharactersAsync(string username, int remainingCharacters)
        {
            Guid packageUsageID = await _userManager.Users
                .Include(x => x.PackageUsage)
                .Where(x => x.UserName == username)
                .Select(x => x.PackageUsage.ID).FirstOrDefaultAsync();
            PackageUsage packageUsage = await _packageUsageReadRepository.GetByIdAsync(packageUsageID.ToString());
            packageUsage.RemainingCharacters = packageUsage.RemainingCharacters - remainingCharacters;
            _packageUsageWriteRepository.Update(packageUsage);
            await _packageUsageWriteRepository.SaveAsync();

        }

    }
}
