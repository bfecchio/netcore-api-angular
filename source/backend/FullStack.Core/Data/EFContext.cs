using FullStack.Core.Extensions;
using FullStack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FullStack.Core.Data
{
    public sealed class EFContext : IdentityDbContext<AppUser, AppRole, string>
    {
        #region Constructors

        public EFContext(DbContextOptions<EFContext> options)
            : base(options)
        { }

        #endregion

        #region Overriden Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.ConfigureIdentity();
            modelBuilder.LoadAllConfigurations();
            modelBuilder.RemoveCascadeDeleteConvention();
        }

        #endregion
    }
}
