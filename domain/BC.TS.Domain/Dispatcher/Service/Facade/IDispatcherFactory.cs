using BC.TS.Domain.Dispatcher.Entity;

namespace BC.TS.Domain.Dispatcher.Service.Facade
{
    public interface IDispatcherFactory
    {
        Task<IEnumerable<Candidate>> GenerateCandidateListAsync(int count);
        Task<IEnumerable<Examiner>> GenerateExaminerListAsync(int count);
    }
}
