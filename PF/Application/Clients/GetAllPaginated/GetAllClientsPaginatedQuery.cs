using MediatR;
using PF.Presentation.Models.Clients;
using PF.Presentation.Models.Shared;

namespace PF.Application.Clients.GetAllPaginated
{
    public class GetAllClientsPaginatedQuery : IRequest<PaginatedList<ClientResult>>
    {
        public PaginationParameters Parameters { get; }

        public GetAllClientsPaginatedQuery(PaginationParameters parameters)
        {
            Parameters = parameters;
        }
    }
}
