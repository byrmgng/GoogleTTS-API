using Google.Cloud.TextToSpeech.V1;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeechAPI.Application.Abstractions.Services;

namespace TextToSpeechAPI.Application.Features.Queries.VoicerQueries.GetVoicerQueries
{
    public class GetVoicerQueryHandler : IRequestHandler<GetVoicerQueryRequest, GetVoicerQueryResponse>
    {
        private readonly IGttsService _gttsService;
        public GetVoicerQueryHandler(IGttsService gttsService)
        {
            _gttsService = gttsService;
        }

        public async Task<GetVoicerQueryResponse> Handle(GetVoicerQueryRequest request, CancellationToken cancellationToken)
        {
            List<Voice> _voices = await _gttsService.GetSupportedVoicesAsync();
            return new()
            {
                Voices = _voices,
            };
        }
    }
}
