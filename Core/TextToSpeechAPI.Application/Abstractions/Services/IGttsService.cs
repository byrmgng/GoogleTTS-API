using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Google.Cloud.TextToSpeech.V1;


namespace TextToSpeechAPI.Application.Abstractions.Services
{
    public interface IGttsService
    {
        public Task<List<Voice>> GetSupportedVoicesAsync();
        public Task<byte[]> SynthesizeSpeechAsync(string username,string text, string languageCode = "en-US", string voiceName = "en-US-Wavenet-D");

    }
}
