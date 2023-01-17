using MediatR;
using PF.Presentation.Models.Clients;

namespace PF.Application.Clients.GetDetails
{
    public class GetClientDetailsQuery : IRequest<ClientDetailsResult>
    {
        public GetClientDetailsQuery(Guid clientId)
        {
            ClientId = clientId;
        }
        public Guid ClientId { get; }
    }
}
