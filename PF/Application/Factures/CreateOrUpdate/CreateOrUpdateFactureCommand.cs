using MediatR;
using PF.Presentation.Models.Factures;

namespace PF.Application.Factures.CreateOrUpdate
{
    public class CreateOrUpdateFactureCommand : IRequest<Guid>
    {
        public CreateOrUpdateFactureCommand(CreateOrUpdateFactureDto dto)
        {
            Id = dto.Id;
            ClientId = dto.ClientId;
            Libelle = dto.Libelle;
            Content = dto.Content;
            Montant = dto.Montant;
        }

        public Guid? Id { get; }
        public Guid? ClientId { get; set; }
        public string Libelle { get; }
        public string Content { get; }
        public double Montant { get; }
    }
}
