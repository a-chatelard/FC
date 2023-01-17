using PF.Domain.Entities;

namespace PF.Presentation.Models.Factures
{
    public class FactureResult
    {
        public FactureResult() { }

        public FactureResult(Facture facture)
        {
            Id = facture.Id;
            Libelle = facture.Libelle;
            Content = facture.Content;
            Montant = facture.Montant;
        }

        public Guid Id { get; }
        public string Libelle { get; }
        public string Content { get; }
        public double Montant { get; }
    }
}
