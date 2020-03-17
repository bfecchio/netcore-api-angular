using FullStack.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FullStack.Core.Business.Validators
{
    internal sealed class TicketValidator : GenericValidator<Ticket>
    {
        #region Constructors

        public TicketValidator(ILogger<TicketValidator> logger)
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
