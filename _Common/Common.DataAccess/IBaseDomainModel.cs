namespace Common.DataAccess.Utilities
{
    public interface IBaseDomainModel<TId>
    {
        TId Id { get; set; }
    }
}
