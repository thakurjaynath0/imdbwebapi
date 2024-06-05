
using ImdbWebApi.Test.MockResources;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace ImdbWebApi.Test.StepDefinitions
{
    [Binding, Scope(Feature = "Movie Resource")]
    public class MovieSteps : BaseSteps
    {
        public MovieSteps(CustomWebApplicationFactory factory)
            : base(factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped( _ => MovieMock.MovieRepositoryMock.Object);
                    services.AddScoped( _ => MovieMock.ProducerRepositoryMock.Object);
                    services.AddScoped( _ => MovieMock.ActorRepositoryMock.Object);
                    services.AddScoped( _ => MovieMock.GenderRepositoryMock.Object);
                    services.AddScoped( _ => MovieMock.GenreRepositoryMock.Object);
                    services.AddScoped( _ => MovieMock.ReviewRepositoryMock.Object);
                });
            }))
        { }

        [BeforeScenario]
        public static void Mocks()
        {
            MovieMock.MockAll();
        }
    }
}
