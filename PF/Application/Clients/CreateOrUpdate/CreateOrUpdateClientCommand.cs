using MediatR;
using PF.Presentation.Models.Clients;

namespace PF.Application.Clients.CreateOrUpdate
{
    public class CreateOrUpdateClientCommand: IRequest<Guid>
    {
        public CreateOrUpdateClientCommand(CreateOrUpdateClientDto dto)
        {
            Id = dto.ClientId;
            Prenom = dto.Prenom;
            Nom = dto.Nom;
        }

        public Guid? Id { get; }
        public string Prenom { get; }
        public string Nom { get; }
    }
}
