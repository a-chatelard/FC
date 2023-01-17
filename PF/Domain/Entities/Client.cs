using System.Collections.ObjectModel;

namespace PF.Domain.Entities
{
    public class Client
    {
        internal Client()
        {
        }

        internal Client(Guid id, string prenom, string nom)
        {
            Id = id;
            Prenom = prenom;
            Nom = nom;
        }

        public Guid Id { get; }
        public string Prenom { get; private set; } = default!;
        public string Nom { get; private set; } = default!;

        public Collection<Facture> Factures { get; set; } = new();

        public static Client Create(string prenom, string nom)
        {
            return new Client(Guid.NewGuid(), prenom, nom);
        }

        public void Update(string prenom, string nom)
        {
            Prenom = prenom;
            Nom = nom;
        }

        public void AddFacture(Facture facture)
        {
            if (Factures.All(f => f.Id != facture.Id))
            {
                Factures.Add(facture);
            }
        }
    }
}
