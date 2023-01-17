using MediatR;
using Microsoft.AspNetCore.Mvc;
using PF.Application.Clients.CreateOrUpdate;
using PF.Application.Clients.GetAllPaginated;
using PF.Application.Clients.GetDetails;
using PF.Domain.Entities;
using PF.Presentation.Models.Clients;
using PF.Presentation.Models.Shared;

namespace PF.Presentation.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ClientResult>>> GetAllClients([FromQuery] PaginationParameters parameters, CancellationToken cancellationToken)
        {
            var query = new GetAllClientsPaginatedQuery(parameters);

            var result = await _mediator.Send(query, cancellationToken);
            
            return Ok(result);
        }

        [HttpGet("{clientId:guid}")]
        public async Task<ActionResult<ClientDetailsResult>> GetClientDetails(Guid clientId, CancellationToken cancellationToken)
        {
            var query = new GetClientDetailsQuery(clientId);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> CreateOrUpdateClient([FromBody] CreateOrUpdateClientDto dto, CancellationToken cancellationToken)
        {
            var command = new CreateOrUpdateClientCommand(dto);

            var clientId = await _mediator.Send(command, cancellationToken);

            return Ok(clientId);
        }
    }
}
