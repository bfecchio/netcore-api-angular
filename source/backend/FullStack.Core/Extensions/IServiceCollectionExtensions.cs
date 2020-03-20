using FluentValidation;
using System.Reflection;
using FullStack.Core.Data;
using FullStack.Domain.Entities;
using Microsoft.AspNetCore.Http;
using FullStack.Domain.Constants;
using Microsoft.AspNetCore.Identity;
using FullStack.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.DataProtection;
using FullStack.Domain.Interfaces.Business;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using FullStack.Domain.Interfaces.Business.Services;
using FullStack.Domain.Interfaces.Data.Repositories;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FullStack.Core.Extensions
{
    public static class IServiceCollectionExtensions
    {
        #region Extension Methods

        public static IServiceCollection AddCoreDependencies(this IServiceCollection services, bool enableSensitiveData = false)
        {                        
            services
                .AddDbContext<EFContext>(options =>
                {                    
                    options.UseInMemoryDatabase(databaseName: nameof(EFContext));
                    options.EnableSensitiveDataLogging(enableSensitiveData);
                });

            services
                .AddIdentity<AppUser, AppRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;
                    options.SignIn.RequireConfirmedAccount = true;
                    options.User.AllowedUserNameCharacters = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._\";
                })
                .AddDefaultUI()
                .AddEntityFrameworkStores<EFContext>()
                .AddErrorDescriber<PortugueseIdentityErrorDescriber>()
                .AddDefaultTokenProviders();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.TryAddScoped(x =>
            {
                var context = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(context);
            });
            
            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, ClaimsIdentityFactory>();
            services.AddDataProtection().SetApplicationName(IdentityServerApiConstants.ResourceName);
            
            services.Scan(scan => scan
                .FromAssemblies(Assembly.GetExecutingAssembly())
                    .AddClasses(c => c.AssignableTo(typeof(IBaseRepository)))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()
                    .AddClasses(c => c.AssignableTo(typeof(IValidator<>)))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()
                    .AddClasses(c => c.AssignableTo(typeof(IBaseService)))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()
                    .AddClasses(c => c.AssignableTo<IUnitOfWork>())
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()
                );

            return services;
        }

        #endregion
    }
}
