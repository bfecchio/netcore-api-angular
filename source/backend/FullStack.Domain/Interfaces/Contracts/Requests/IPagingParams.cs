namespace FullStack.Domain.Interfaces.Contracts.Requests
{
    public interface IPagingParams
    {
        #region IPagingParams Members

        int PageSize { get; set; }
        int PageIndex { get; set; }

        #endregion
    }
}
