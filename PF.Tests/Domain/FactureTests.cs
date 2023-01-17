using FluentAssertions;
using PF.Domain.Entities;

namespace PF.Tests.Domain
{
    public class FactureTests
    {
        [Fact]
        public void CreateShouldReturnACorrespondingObject()
        {
            Guid clientId = Guid.NewGuid();
            const string libelle = "LibelleTest";
            const string content = "ContentTest";
            const double montant = 100d;

            var facture = Facture.Create(clientId, libelle, content, montant);

            facture.ClientId.Should().Be(clientId);
            facture.Libelle.Should().Be(libelle);
            facture.Content.Should().Be(content);
            facture.Montant.Should().Be(montant);
        }

        [Fact]
        public void UpdateShouldSetTheRightProperties()
        {
            var facture = Facture.Create(Guid.NewGuid(), "LibelleTest", "ContentTest", 100d);

            const string content = "UpdatedContentTest";
            const double montant = 150d;

            facture.Update(content, montant);

            facture.Content.Should().Be(content);
            facture.Montant.Should().Be(montant);
        }
    }
}
