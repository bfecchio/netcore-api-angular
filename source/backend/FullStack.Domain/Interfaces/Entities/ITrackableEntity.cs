using System;

namespace FullStack.Domain.Interfaces.Entities
{
    public interface ITrackableEntity<TKey> : IBaseEntity
    {
        #region ITrackableEntity Members

        TKey Id { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        string ModifiedBy { get; set; }
        DateTime? ModifiedDate { get; set; }

        #endregion
    }
}
