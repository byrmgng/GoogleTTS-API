using Google.Cloud.TextToSpeech.V1;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeechAPI.Application.Features.Queries.TTSQueries.GetSpeechQueries
{
    public class GetSpeechQueryRequest:IRequest<GetSpeechQueryResponse>
    {
        public string Username { get; set; }
        public string Text { get; set; } = "Metin Girilmedi";
        public string LanguageCode { get; set; } = "en-US";
        public string VoiceName { get; set; } = "en-US-Wavenet-D";
    }
}
