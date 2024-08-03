using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeechAPI.Application.ViewModels.User
{
    public class VM_Speech
    {
        public string Text { get; set; } = "Metin Girilmedi";
        public string LanguageCode { get; set; } = "en-US";
        public string VoiceName { get; set; } = "en-US-Wavenet-D";
    }
}
