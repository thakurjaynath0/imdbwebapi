using ImdbWebApi.Test.MockResources;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace ImdbWebApi.Test.StepDefinitions
{
    [Binding, Scope(Feature = "Actor Resource")]
    public class ActorSteps : BaseSteps
    {
        public ActorSteps(CustomWebApplicationFactory factory)
            : base(factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped( _ => ActorMock.ActorRepositoryMock.Object); 
                    services.AddScoped( _ => ActorMock.GenderRepositoryMock.Object);
                    services.AddScoped( _ => ActorMock.MovieRepositoryMock.Object);

                    services.AddScoped( _ => ActorMock.GenreRepositoryMock.Object);
                    services.AddScoped( _ => ActorMock.ProducerRepositoryMock.Object);  
                });
            }))
        { }

        [BeforeScenario]
        public static void Mocks()
        {
            ActorMock.MockAll();
        }
    }
}
