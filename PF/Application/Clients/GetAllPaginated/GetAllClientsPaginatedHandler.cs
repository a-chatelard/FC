using MediatR;
using Microsoft.EntityFrameworkCore;
using PF.Infrastructure;
using PF.Presentation.Models.Clients;
using PF.Presentation.Models.Shared;

namespace PF.Application.Clients.GetAllPaginated
{
    public class GetAllClientsPaginatedHandler : IRequestHandler<GetAllClientsPaginatedQuery, PaginatedList<ClientResult>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllClientsPaginatedHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<ClientResult>> Handle(GetAllClientsPaginatedQuery request, CancellationToken cancellationToken)
        {
            return await PaginatedList<ClientResult>.CreateAsync(_context.Clients.AsNoTracking().Select(c => new ClientResult(c)).AsQueryable(), request.Parameters);
        }
    }
}
