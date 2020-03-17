using System;
using FluentValidation;
using Microsoft.Extensions.Logging;
using FullStack.Domain.Interfaces.Entities;

namespace FullStack.Core.Business.Validators
{
    public abstract class GenericValidator<TEntity> : AbstractValidator<TEntity>
        where TEntity : class, IBaseEntity
    {
        #region Protected Read-Only Fields

        protected readonly ILogger _logger;

        #endregion

        #region Constructors

        protected GenericValidator(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Configure();
        }

        #endregion

        #region Public Abstract Methods

        public abstract void Configure();

        #endregion
    }
}
