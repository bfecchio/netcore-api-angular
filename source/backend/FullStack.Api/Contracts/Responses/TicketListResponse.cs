using System.Collections.Generic;
using FullStack.Api.Contracts.Schemas;
using FullStack.Domain.Contracts.Responses;

namespace FullStack.Api.Contracts.Responses
{
    public class TicketListResponse : PaginatedResponse<TicketSchema>
    {
        #region Constructors

        public TicketListResponse(int pageIndex, int pageSize, int length)
            : base(pageIndex, pageSize, length)
        { }

        public TicketListResponse(int pageIndex, int pageSize, int length, IEnumerable<TicketSchema> data)
            : base(pageIndex, pageSize, length, data)
        { }

        #endregion
    }
}
