namespace FullStack.Domain.Interfaces.Entities
{
    public interface IEntity<TKey> : IBaseEntity
    {
        #region IEntity Members

        TKey Id { get; set; }

        #endregion
    }
}
