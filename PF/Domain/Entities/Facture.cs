namespace PF.Domain.Entities
{
    public class Facture
    {
        internal Facture()
        {
        }

        internal Facture(Guid id, Guid clientId, string libelle)
        {
            Id = id;
            ClientId = clientId;
            Libelle = libelle;
        }

        public Guid Id { get; }
        public Guid ClientId { get; }
        public string Libelle { get; } = default!;
        public string Content { get; private set; } = default!;
        public double Montant { get; private set; }

        public static Facture Create(Guid clientId, string libelle, string content, double montant)
        {
            var facture = new Facture(Guid.NewGuid(), clientId, libelle);
            facture.Update(content, montant);
            return facture;
        }

        public void Update(string content, double montant)
        {
            Content = content;
            Montant = montant;
        }

        public double GetMontantEstime(double taux)
        {
            return Montant * taux;
        }
    }
}
