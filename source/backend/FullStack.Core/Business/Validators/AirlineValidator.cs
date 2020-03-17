using FullStack.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FullStack.Core.Business.Validators
{
    internal sealed class AirlineValidator : GenericValidator<Airline>
    {
        #region Constructors

        public AirlineValidator(ILogger<AirlineValidator> logger)
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
