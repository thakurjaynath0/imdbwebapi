
using ImdbWebApi.Test.MockResources;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace ImdbWebApi.Test.StepDefinitions
{
    [Binding, Scope(Feature = "Genre Resource")]
    public class GenreSteps : BaseSteps
    {
        public GenreSteps(CustomWebApplicationFactory factory)
            : base(factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped( _ => GenreMock.GenreRepositoryMock.Object);
                    services.AddScoped( _ => GenreMock.MovieRepositoryMock.Object);  

                    services.AddScoped( _ => GenreMock.GenderRepositoryMock.Object);    
                    services.AddScoped( _ => GenreMock.ActorRepositoryMock.Object);
                    services.AddScoped( _ => GenreMock.ProducerRepositoryMock.Object);
                });
            }))
        { }

        [BeforeScenario]
        public static void Mocks()
        {
            GenreMock.MockAll();
        }
    }
}
