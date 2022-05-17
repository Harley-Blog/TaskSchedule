namespace BC.TS.Domain.Facade
{
    public interface IUnitOfWork
    {
        bool Commit();
        Task<bool> CommitAsync();
    }
}
