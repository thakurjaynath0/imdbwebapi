using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ImdbWebApi.Repositories;
using ImdbWebApi.Repositories.Interfaces;
using ImdbWebApi.Services;
using ImdbWebApi.Services.Interfaces;
using ImdbWebApi.Utils.ModelMappers;
using ImdbWebApiComplete;
using Microsoft.Extensions.Configuration;
using ImdbWebApi.Middlewares;

namespace ImdbWebApi
{
    public class Startup
    {
        public readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration; 
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<ConnectionString>(Configuration.GetSection("ConnectionStrings"));

            services.AddSingleton<IActorRepository, ActorRepository>();
            services.AddSingleton<IActorService, ActorService>();

            services.AddSingleton<IGenderRepository, GenderRepository>();    
            services.AddSingleton<IGenderService, GenderService>();  

            services.AddSingleton<IProducerRepository, ProducerRepository>();    
            services.AddSingleton<IProducerService, ProducerService>();  

            services.AddSingleton<IGenreRepository, GenreRepository>();  
            services.AddSingleton<IGenreService, GenreService>();    

            services.AddSingleton<IMovieRepository, MovieRepository>();  
            services.AddSingleton<IMovieService, MovieService>();

            services.AddSingleton<IReviewRepository, ReviewRepository>();    
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
