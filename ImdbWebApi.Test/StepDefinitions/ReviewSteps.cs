
using ImdbWebApi.Test.MockResources;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace ImdbWebApi.Test.StepDefinitions
{
    [Binding, Scope(Feature = "Review Resource")]
    public class ReviewSteps : BaseSteps
    {
        public ReviewSteps(CustomWebApplicationFactory factory)
            : base(factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped( _ => ReviewMock.ReviewRepositoryMock.Object);
                    services.AddScoped( _ => ReviewMock.MovieRepsitoryMock.Object);

                    services.AddScoped( _ => ReviewMock.GenderRepositoryMock.Object);   
                    services.AddScoped( _ => ReviewMock.ActorRepositoryMock.Object);   
                    services.AddScoped( _ => ReviewMock.ProducerRepositoryMock.Object);   
                    services.AddScoped( _ => ReviewMock.GenreRepositoryMock.Object);   
                });
            }))
        { }

        [BeforeScenario]
        public static void Mocks()
        {
            ReviewMock.MockAll();
        }
    }
}
