using PF.Domain.Entities;

namespace PF.Presentation.Models.Clients
{
    public class ClientResult
    {
        public ClientResult() { }

        public ClientResult(Client client)
        {
            Id = client.Id;
            Prenom = client.Prenom;
            Nom = client.Nom;
        }

        public Guid Id { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
    }
}
