using System;
using FullStack.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FullStack.Core.Helpers
{
    public sealed class DbGeneratorHelper
    {
        #region Public Static Methods

        public static void Create(IServiceProvider serviceProvider)
        {
            using (var dbContext = new EFContext(serviceProvider.GetRequiredService<DbContextOptions<EFContext>>()))
            {
                if (dbContext.Database.IsInMemory())
                    dbContext.Database.EnsureCreated();
            }
        }

        #endregion
    }
}
