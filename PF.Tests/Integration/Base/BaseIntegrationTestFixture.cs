using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PF.Infrastructure;

namespace PF.Tests.Integration.Base
{
    public class BaseIntegrationTestFixture : IDisposable
    {
        public BaseIntegrationTestFixture()
        {
            var application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => {
                builder.ConfigureTestServices(services => {
                    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                    .UseInMemoryDatabase("PF_db")
                                    .Options;
                    services.AddSingleton(options);
                    services.AddSingleton<ApplicationDbContext>();
                });
            });

            var scopeFactory = application.Services.GetService<IServiceScopeFactory>();
            Scope = scopeFactory.CreateScope();
            Context = Scope.ServiceProvider.GetService<ApplicationDbContext>();

            HttpClient = application.CreateClient();
        }

        public HttpClient HttpClient { get; }
        public ApplicationDbContext Context { get; }

        private IServiceScope Scope;

        public void Dispose()
        {
            Scope.Dispose();
            HttpClient?.Dispose();
            Context?.Dispose();
        }
    }
}
