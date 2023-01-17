using MediatR;
using Microsoft.EntityFrameworkCore;
using PF.Application.Exceptions;
using PF.Domain.Entities;
using PF.Infrastructure;

namespace PF.Application.Clients.CreateOrUpdate
{
    public class CreateOrUpdateClientHandler : IRequestHandler<CreateOrUpdateClientCommand, Guid>
    {
        private readonly ApplicationDbContext _context;

        public CreateOrUpdateClientHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateOrUpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (client != null)
            {
                client.Update(request.Prenom, request.Nom);
            }
            else if (request.Id.HasValue)
            {
                throw new EntityNotFoundException(request.Id.Value, nameof(Client));
            }
            else 
            {
                client = Client.Create(request.Prenom, request.Nom);
                _context.Clients.Add(client);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return client.Id;
        }
    }
}
