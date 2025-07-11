using Application.Features.UserMeterProccessing.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskForEnsek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MetersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("meter-reading-uploads")]
        public async Task<ReadingRateDataViewModel> AddMeterReadings([FromBody] AddUserMetersCommand addUserMetersCommand)
        {
            return await _mediator.Send(addUserMetersCommand);
        }
    }
}
