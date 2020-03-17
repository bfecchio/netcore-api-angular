using FullStack.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FullStack.Core.Business.Validators
{
    internal sealed class AirportValidator : GenericValidator<Airport>
    {
        #region Constructors

        public AirportValidator(ILogger<AirportValidator> logger)
            : base(logger)
        { }

        #endregion

        #region Overriden Methods

        public override void Configure()
        {
            
        }

        #endregion
    }
}
