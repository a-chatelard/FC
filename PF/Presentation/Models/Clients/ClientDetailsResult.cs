using System.Collections.Immutable;
using PF.Domain.Entities;
using PF.Presentation.Models.Factures;

namespace PF.Presentation.Models.Clients
{
    public class ClientDetailsResult : ClientResult
    {
        public ClientDetailsResult() { }

        public ClientDetailsResult(Client client) : base(client)
        {
            Factures = client.Factures.Select(f => new FactureResult(f)).ToImmutableList();
            MontantFactures = Factures.Sum(f => f.Montant);
        }

        public ImmutableList<FactureResult> Factures { get; }

        public double MontantFactures { get; }
    }
}
