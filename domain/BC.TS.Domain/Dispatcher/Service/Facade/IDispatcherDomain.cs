using BC.TS.Domain.Dispatcher.Entity;

namespace BC.TS.Domain.Dispatcher.Service.Facade
{
    public interface IDispatcherDomain
    {
        Task<IEnumerable<Examiner>> ExecuteAppraiseAsync();
    }
}
