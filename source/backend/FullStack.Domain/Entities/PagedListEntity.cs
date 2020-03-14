using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using FullStack.Domain.Interfaces.Entities;

namespace FullStack.Domain.Entities
{
    [DataContract]
    [Serializable]
    public sealed class PagedListEntity<TEntity> : IPagedListEntity<TEntity>
            where TEntity : class, IBaseEntity
    {
        #region Public Properties

        [DataMember]
        public int Total { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public int PageIndex { get; set; }

        [DataMember]
        public IEnumerable<TEntity> Collection { get; set; }

        #endregion

        #region Constructors

        public PagedListEntity(int pageIndex, int pageSize, int total = 0)
        {
            Total = total;
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        #endregion
    }
}
