using System;
using System.Collections.Generic;
using System.Text;

namespace FullStack.Domain.Interfaces.Entities
{
    public interface IPagedListEntity<TEntity>
        where TEntity : class, IBaseEntity
    {
        #region Public Propeties

        int Total { get; set; }
        int PageSize { get; set; }
        int PageIndex { get; set; }
        IEnumerable<TEntity> Collection { get; set; }

        #endregion
    }
}
