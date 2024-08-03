using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Rpc.Context.AttributeContext.Types;
using TextToSpeechAPI.Application.Abstractions.Services;
using MediatR;

namespace TextToSpeechAPI.Application.Features.Queries.TTSQueries.GetSpeechQueries
{
    public class GetSpeechQueryHandler : IRequestHandler<GetSpeechQueryRequest, GetSpeechQueryResponse>
    {
        private readonly IGttsService _gttsService;

        public GetSpeechQueryHandler(IGttsService gttsService)
        {
            _gttsService = gttsService;

        }

        public async Task<GetSpeechQueryResponse> Handle(GetSpeechQueryRequest request, CancellationToken cancellationToken)
        {
            var audioBytes = await _gttsService.SynthesizeSpeechAsync(request.Username,request.Text,request.LanguageCode,request.VoiceName);

            return new GetSpeechQueryResponse
            {
                AudioBytes = audioBytes
            };
        }
    };
}

