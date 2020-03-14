using NSwag;
using System;
using System.IO;
using AutoMapper;
using System.Linq;
using Newtonsoft.Json;
using System.Reflection;
using System.Globalization;
using System.IO.Compression;
using FullStack.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using FullStack.Domain.Entities;
using FullStack.Core.Extensions;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using FullStack.Domain.Constants;
using FullStack.Api.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authorization;
using NSwag.Generation.Processors.Security;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography.X509Certificates;

namespace FullStack.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        #region Extension Methods

        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(options =>
            {
                options.AllowNullCollections = true;
                options.AllowNullDestinationValues = true;
                options.AddMaps(Assembly.GetExecutingAssembly());
            });

            services.AddSingleton(config.CreateMapper());

            return services;
        }

        public static IServiceCollection ConfigureCaching(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddResponseCaching();

            return services;
        }

        public static IServiceCollection ConfigureCulture(this IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("pt-BR");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("pt-BR") };
                options.RequestCultureProviders.Clear();
            });

            return services;
        }

        public static IServiceCollection ConfigureCompression(this IServiceCollection services)
        {
            services
                .Configure<GzipCompressionProviderOptions>(x => x.Level = CompressionLevel.Optimal)
                .Configure<BrotliCompressionProviderOptions>(x => x.Level = CompressionLevel.Optimal);

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/png", "image/jpg", "image/jpeg" });
                options.EnableForHttps = true;
            });

            return services;
        }

        public static IServiceCollection ConfigureMvc(this IServiceCollection services)
        {
            services
                .AddMvcCore(options =>
                {
                    var schemaPolicy = new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build();

                    options.EnableEndpointRouting = true;
                    options.Filters.Add(new AuthorizeFilter(schemaPolicy));
                })
                .AddDataAnnotations()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.None;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.Converters.Add(new IsoDateTimeConverter());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var response = context.ModelState.ToErrorResponse();
                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }

        public static IServiceCollection ConfigureIdentityServer(this IServiceCollection services)
        {
            var hostingEnviroment = services.BuildServiceProvider().GetService<IWebHostEnvironment>();

            services
                .AddIdentityServer()
                .AddSigningCredential(new X509Certificate2(Path.Combine(hostingEnviroment.ContentRootPath, "Certs", IdentityServerApiConstants.SecurityConstants.PfxName)))
                .AddInMemoryApiResources(IdentityServerSettings.GetApiResources())
                .AddInMemoryClients(IdentityServerSettings.GetClients())
                .AddAspNetIdentity<AspNetUser>()
                .AddCustomUserStore();

            return services;
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            var availableScopes = new Dictionary<string, string>();

            ConfigHelper.TryGet<SwaggerConfig>(nameof(SwaggerConfig), out var swaggerConfig);
            ConfigHelper.TryGet<IdentityServerConfig>(nameof(IdentityServerConfig), out var identityConfig);

            services.AddSwaggerDocument(config =>
            {
                config.Title = swaggerConfig.Title;
                config.Version = swaggerConfig.Version;
                config.Description = swaggerConfig.Description;

                config.PostProcess = document =>
                {
                    document.Info.Title = swaggerConfig.Title;
                    document.Info.Version = $"v{swaggerConfig.Version}";
                    document.Info.Description = swaggerConfig.Description;
                    document.Schemes = new OpenApiSchema[] { OpenApiSchema.Https };
                };

                config.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT"));

                config.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT", availableScopes.Keys.ToList(), new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Flow = OpenApiOAuth2Flow.Password,
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    TokenUrl = identityConfig.TokenUrl,
                    AuthorizationUrl = identityConfig.AuthorizationUrl,
                    Scopes = availableScopes
                }));
            });

            return services;
        }

        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(IdentityServerApiConstants.ResourceName, p => p.RequireScope(IdentityServerApiConstants.ResourceName));
                // TODO: se necessário, acrescentar as demais policies customizadas
            });

            return services;
        }

        public static IServiceCollection ConfigureApiVersion(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(options =>
            {
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            return services;
        }

        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
        {
            var hostingEnviroment = services.BuildServiceProvider().GetService<IWebHostEnvironment>();
            var certificate = new X509Certificate2(Path.Combine(hostingEnviroment.ContentRootPath, "Certs", IdentityServerApiConstants.SecurityConstants.PfxName));

            ConfigHelper.TryGet<IdentityServerConfig>(nameof(IdentityServerConfig), out var configuration);

            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.Authority = configuration.Authority;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidAudiences = new string[] { IdentityServerApiConstants.ResourceName },
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new X509SecurityKey(certificate),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }

        #endregion
    }
}
