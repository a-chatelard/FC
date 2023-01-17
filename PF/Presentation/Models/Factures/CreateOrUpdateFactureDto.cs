namespace PF.Presentation.Models.Factures
{
    public class CreateOrUpdateFactureDto
    {
        public CreateOrUpdateFactureDto(Guid? id, Guid? clientId, string libelle, string content, double montant)
        {
            Id = id;
            ClientId = clientId;
            Libelle = libelle;
            Content = content;
            Montant = montant;
        }

        public Guid? Id { get; }
        public Guid? ClientId { get; set; }
        public string Libelle { get; }
        public string Content { get; }
        public double Montant { get; }
    }
}
