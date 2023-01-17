using MediatR;
using Microsoft.EntityFrameworkCore;
using PF.Application.Exceptions;
using PF.Domain.Entities;
using PF.Infrastructure;
using PF.Presentation.Models.Clients;

namespace PF.Application.Clients.GetDetails
{
    public class GetClientDetailsHandler : IRequestHandler<GetClientDetailsQuery, ClientDetailsResult>
    {
        private readonly ApplicationDbContext _context;

        public GetClientDetailsHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ClientDetailsResult> Handle(GetClientDetailsQuery request, CancellationToken cancellationToken)
        {
            var client = await _context.Clients.Include(c => c.Factures).FirstOrDefaultAsync(c => c.Id == request.ClientId, cancellationToken);

            if (client == null)
            {
                throw new EntityNotFoundException(request.ClientId, nameof(Client));
            }

            return new ClientDetailsResult(client);
        }
    }
}
