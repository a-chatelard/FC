using MediatR;
using Microsoft.EntityFrameworkCore;
using PF.Application.Exceptions;
using PF.Domain.Entities;
using PF.Infrastructure;

namespace PF.Application.Factures.CreateOrUpdate
{
    public class CreateOrUpdateFactureHandler : IRequestHandler<CreateOrUpdateFactureCommand, Guid>
    {
        private ApplicationDbContext _context;

        public CreateOrUpdateFactureHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateOrUpdateFactureCommand request, CancellationToken cancellationToken)
        {
            var facture = await _context.Factures.FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

            if (facture!= null)
            {
                facture.Update(request.Content, request.Montant);
            }
            else if (request.Id.HasValue)
            {
                throw new EntityNotFoundException(request.Id.Value, nameof(Client));
            }
            else
            {
                var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == request.ClientId, cancellationToken);
                if (client == null)
                {
                    throw new EntityNotFoundException(request.ClientId.Value, nameof(Client));
                }

                facture = Facture.Create(client.Id, request.Libelle, request.Content, request.Montant);

                _context.Factures.Add(facture);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return facture.Id;
        }
    }
}
