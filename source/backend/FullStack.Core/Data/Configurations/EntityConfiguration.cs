using System;
using System.Collections.Generic;
using FullStack.Domain.Interfaces.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullStack.Core.Data.Configurations
{
    internal abstract class EntityConfiguration<TEntity>
        where TEntity : class, IBaseEntity
    {
        #region Public Abstract Methods

        public abstract void Configure(EntityTypeBuilder<TEntity> builder);

        public virtual IEnumerable<TEntity> Seed() => Array.Empty<TEntity>();

        #endregion
    }
}
