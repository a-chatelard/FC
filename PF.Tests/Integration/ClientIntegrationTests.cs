using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PF.Domain.Entities;
using PF.Infrastructure;
using PF.Presentation.Models.Clients;
using PF.Tests.Integration.Base;
using PF.Tests.Integration.Helpers;
using System.Net.Http;
using System.Text;

namespace PF.Tests.Integration
{
    public class ClientControllerTests : IClassFixture<ClientControllerTestsFixture>
    {
        private readonly ApplicationDbContext _context;
        private readonly ClientControllerTestsFixture _fixture;
        private readonly HttpClient _httpClient;

        public ClientControllerTests(ClientControllerTestsFixture fixture)
        {
            _fixture = fixture;
            _context = fixture.Context;
            _httpClient = fixture.HttpClient;
        }

        [Fact]
        public async Task GetAllShouldReturn200AndContainOneClient()
        {
            var response = await _httpClient.GetAsync("/client");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var parsedResult = JsonConvert.DeserializeObject<List<ClientResult>>(result);

            parsedResult.Count.Should().Be(1);

            var client = parsedResult.First();

            client.Should().BeEquivalentTo(_fixture.ExistingClient, options => options.Excluding(c => c.Factures));
        }

        [Fact]
        public async Task GetByIdShouldReturn200AndContainOneClient()
        {
            var response = await _httpClient.GetAsync("/client/" + _fixture.ExistingClient.Id);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var parsedResult = JsonConvert.DeserializeObject<ClientDetailsResult>(result);

            parsedResult.Should().BeEquivalentTo(_fixture.ExistingClient, options => options.Excluding(c => c.Factures));
        }

        [Fact]
        public async Task PostClientShouldAddItToTheDatabaseAndReturnItsID()
        {
            var client = new CreateOrUpdateClientDto(null, "TestPrenom", "TestNom");

            var stringContent = JsonHelper.ToStringContent(client);

            var response = await _httpClient.PutAsync("/client", stringContent);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var parsedResult = JsonConvert.DeserializeObject<Guid>(result);

            parsedResult.Should().NotBeEmpty();

            var createdClient = await _context.Clients.FirstOrDefaultAsync(c => c.Id == parsedResult);
            createdClient.Should().NotBeNull();
        }
    }

    public class ClientControllerTestsFixture : BaseIntegrationTestFixture
    {
        public Client ExistingClient { get; private set; }

        public ClientControllerTestsFixture() : base()
        {
            SeedData();
        }

        public void SeedData() 
        {
            ExistingClient = Client.Create("TestPrenom", "TestNom");

            Context.Clients.Add(ExistingClient);
            Context.Factures.Add(Facture.Create(ExistingClient.Id, "TestLibelle", "TestContent", 100d));

            Context.SaveChanges();
        }
    }
}
