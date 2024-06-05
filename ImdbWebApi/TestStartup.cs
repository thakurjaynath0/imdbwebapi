using ImdbWebApi.Middlewares;
using ImdbWebApi.Services.Interfaces;
using ImdbWebApi.Services;
using ImdbWebApi.Utils.ModelMappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ImdbWebApi
{
    public class TestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IActorService, ActorService>();
            services.AddSingleton<IGenderService, GenderService>();
            services.AddSingleton<IProducerService, ProducerService>();
            services.AddSingleton<IGenreService, GenreService>();
            services.AddSingleton<IMovieService, MovieService>();
            services.AddSingleton<IReviewService, ReviewService>();
            services.AddSingleton<IFileUploadService, FileUploadService>();

            services.AddSingleton<IModelMapper, ModelMapper>();
            services.AddSingleton<CustomExceptionHandlerMiddleware>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();    
            }

            app.UseRouting();
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
