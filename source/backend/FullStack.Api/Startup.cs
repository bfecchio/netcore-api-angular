using Serilog;
using FullStack.Api.Extensions;
using FullStack.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FullStack.Api
{
    public class Startup
    {
        #region Public Properties

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostingEnvironment { get; }

        #endregion

        #region Constructors

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnviroment)
        {
            Configuration = configuration;
            WebHostingEnvironment = webHostEnviroment;
        }

        #endregion

        #region Public Methods

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton(Configuration)
                .AddSingleton(WebHostingEnvironment);

            services
                .AddCoreDependencies(enableSensitiveData: WebHostingEnvironment.IsDevelopment());

            services
                .AddCors()
                .AddControllers();

            services
                .ConfigureCulture()
                .ConfigureCaching()
                .ConfigureCompression()
                .ConfigureMvc()
                .ConfigureAutoMapper()
                .ConfigureApiVersion()
                .ConfigureAuthorization()
                .ConfigureIdentityServer()
                .ConfigureAuthentication()
                .ConfigureSwagger();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (WebHostingEnvironment.IsDevelopment())
            {
            }
            else
            {
                app.UseHsts();
            }

            app
                .UseHttpsRedirection()
                .UseResponseCompression()
                .UseSerilogRequestLogging()
                .UseMiddlewareConfiguration()
                .UseStatusCodePagesConfiguration()
                .UseRouting()
                .UseCors(options => options
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Content-Disposition")
                )
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(options =>
                {
                    options.MapControllers();
                })
                .UseCultureConfiguration()
                .UseResponseCaching()
                .UseIdentityServer()
                .UseSwaggerConfiguration();
        }

        #endregion
    }
}
