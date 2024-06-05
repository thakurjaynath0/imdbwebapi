
using ImdbWebApi.Test.MockResources;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace ImdbWebApi.Test.StepDefinitions
{
    [Binding, Scope(Feature = "Producer Resource")]
    public class ProducerSteps : BaseSteps
    {
        public ProducerSteps(CustomWebApplicationFactory factory)
        : base(factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped( _ => ProducerMock.ProducerRepositoryMock.Object);
                services.AddScoped( _ => ProducerMock.GenderRepositoryMock.Object);
                services.AddScoped( _ => ProducerMock.MovieRepositoryMock.Object);

                services.AddScoped( _ => ProducerMock.ActorRepositoryMock.Object);  
                services.AddScoped( _ => ProducerMock.GenreRepositoryMock.Object);  
            });
        }))
        { }

        [BeforeScenario]
        public static void Mocks()
        {
            ProducerMock.MockAll();
        }
    }
}
