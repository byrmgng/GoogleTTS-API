using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeechAPI.Application.Features.Queries.VoicerQueries.GetVoicerQueries
{
    public class GetVoicerQueryRequest:IRequest<GetVoicerQueryResponse>
    {
    }
}
