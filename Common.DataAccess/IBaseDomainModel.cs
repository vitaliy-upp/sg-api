namespace Common.DataAccess
{
    public interface IBaseDomainModel<TId>
    {
        TId Id { get; set; }
    }
}
