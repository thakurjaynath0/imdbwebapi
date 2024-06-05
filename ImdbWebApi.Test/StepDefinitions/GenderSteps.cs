
using ImdbWebApi.Test.MockResources;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;
using Xunit.Abstractions;

namespace ImdbWebApi.Test.StepDefinitions
{
    [Binding, Scope(Feature = "Gender Resource")]
    public class GenderSteps : BaseSteps
    {
        public GenderSteps(CustomWebApplicationFactory factory)
            : base(factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped( _ => GenderMock.GenderRepositoryMock.Object);
                    services.AddScoped( _ => GenderMock.ActorRepositoryMock.Object);
                    services.AddScoped( _ => GenderMock.ProducerRepositoryMock.Object);   

                    services.AddScoped( _ => GenderMock.GenreRepositoryMock.Object);
                });
            }))
        { }


        [BeforeScenario]
        public static void Mocks()
        {
            GenderMock.MockAll();
        }
    }
}
