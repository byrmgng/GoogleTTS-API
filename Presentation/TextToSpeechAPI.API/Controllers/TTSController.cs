using Google.Cloud.TextToSpeech.V1;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextToSpeechAPI.Application.Abstractions.Services;
using TextToSpeechAPI.Application.Features.Queries.TTSQueries.GetSpeechQueries;
using TextToSpeechAPI.Application.Features.Queries.VoicerQueries.GetVoicerQueries;
using TextToSpeechAPI.Application.ViewModels.User;
using TextToSpeechAPI.Infrastructure.Services;

namespace TextToSpeechAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TTSController : ControllerBase
    {
        readonly IMediator _mediator;
        public TTSController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetVoicer()
        {
            GetVoicerQueryRequest request = new GetVoicerQueryRequest();
            GetVoicerQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetSpeech(VM_Speech vmSpeech)
        {
            GetSpeechQueryRequest request = new GetSpeechQueryRequest() {
                LanguageCode = vmSpeech.LanguageCode,
                Text = vmSpeech.Text,
                VoiceName = vmSpeech.VoiceName,
                Username = User.Identity.Name
            };
            GetSpeechQueryResponse response = null;
            if (request.Username != null) {
                response = await _mediator.Send(request);
            }
            return Ok(response);
        }
    }
}
