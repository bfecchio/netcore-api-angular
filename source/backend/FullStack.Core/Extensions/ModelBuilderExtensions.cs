using System;
using System.Linq;
using System.Reflection;
using FullStack.Core.Helpers;
using FullStack.Domain.Entities;
using System.Collections.Generic;
using FullStack.Domain.Enumerations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FullStack.Core.Data.Configurations;
using FullStack.Domain.Interfaces.Entities;
using FullStack.Domain.Infrastructure.Config;

namespace FullStack.Core.Extensions
{
    internal static class ModelBuilderExtensions
    {
        #region Extension Methods

        public static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder, EntityConfiguration<TEntity> entityConfiguration)
            where TEntity : class, IBaseEntity
        {
            modelBuilder.Entity<TEntity>(entityConfiguration.Configure);
            modelBuilder.Entity<TEntity>().HasData(entityConfiguration.Seed());
        }

        public static void LoadAllConfigurations(this ModelBuilder modelBuilder)
        {
            var assemblies = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x =>
                       x.Name.EndsWith("Configuration", StringComparison.OrdinalIgnoreCase)
                    && x.Namespace.EndsWith("Data.Configurations", StringComparison.OrdinalIgnoreCase)
                );

            foreach (var assembly in assemblies)
            {
                dynamic instance = Activator.CreateInstance(assembly);
                ModelBuilderExtensions.AddConfiguration(modelBuilder, instance);
            }
        }

        public static void RemoveCascadeDeleteConvention(this ModelBuilder modelBuilder)
        {
            var imutableEntities = modelBuilder.Model.GetEntityTypes()
                .Where(e => !e.IsOwned())
                .SelectMany(e => e.GetForeignKeys());

            foreach (var relationship in imutableEntities)
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public static void ConfigureIdentity(this ModelBuilder modelBuilder)
        {
            ConfigHelper.TryGet<GlobalConfig>(nameof(GlobalConfig), out var config);

            modelBuilder.Entity<AppUser>(p =>
            {
                p.HasMany(x => x.Roles)
                    .WithOne()
                    .HasForeignKey(ur => ur.UserId).IsRequired();

                var usuario = new AppUser
                {
                    Id = Guid.Empty.ToString(),                    
                    UserName = @"local\admin",
                    NormalizedUserName = @"LOCAL\ADMIN",
                    Email = "no-reply@test-fullstack.com.br",
                    NormalizedEmail = "NO-REPLY@TEST-FULLSTACK.COM.BR",
                    EmailConfirmed = true,
                    LockoutEnabled = false                    
                };

                usuario.SecurityStamp = Guid.Empty.ToString();
                usuario.ConcurrencyStamp = Guid.Empty.ToString();
                usuario.PasswordHash = new PasswordHasher<AppUser>()
                    .HashPassword(usuario, config.DefaultAdminPassword);

                p.HasData(usuario);
            });
            
            modelBuilder.Entity<AppRole>(p =>
            {
                var collection = LoadRoles();

                p.HasMany(x => x.Users)
                    .WithOne()
                    .HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Restrict).IsRequired();
                    
                p.HasData(collection);
            });

            modelBuilder.Entity<IdentityUserRole<string>>(p =>
            {
                p.HasData(new IdentityUserRole<string>[]
                {
                    new IdentityUserRole<string>
                    {
                          UserId = Guid.Empty.ToString()
                        , RoleId = ((int)EnumRoles.Administrator).ToString()
                    }
                });
            });
        }

        #endregion

        #region Private Methods

        private static IEnumerable<AppRole> LoadRoles()
        {
            foreach (var item in Enum.GetValues(typeof(EnumRoles)))
                yield return new AppRole
                {
                      Id = ((int)item).ToString()
                    , Name = item.ToString()
                    , NormalizedName = item.ToString().ToUpper()
                    , ConcurrencyStamp = Guid.Empty.ToString()
                };
        }

        #endregion
    }
}
