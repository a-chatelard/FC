namespace PF.Presentation.Models.Clients
{
    public class CreateOrUpdateClientDto
    {
        public CreateOrUpdateClientDto(Guid? clientId, string prenom, string nom)
        {
            ClientId = clientId;
            Prenom = prenom;
            Nom = nom;
        }

        public Guid? ClientId { get; }
        public string Prenom { get; }
        public string Nom { get; }
    }
}
