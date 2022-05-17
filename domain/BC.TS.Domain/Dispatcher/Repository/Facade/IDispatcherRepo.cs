using BC.TS.Domain.Dispatcher.Repository.PersistenceObject;

namespace BC.TS.Domain.Dispatcher.Repository.Facade
{
    public interface IDispatcherRepo
    {
        Task AddCandidatesAsync(IEnumerable<CandidatePo> entities);
        Task AddExaminersAsync(IEnumerable<ExaminerPo> entities);
        Task<CandidatePo?> GetAvailableCandidateAsync();
        Task<ExaminerPo?> GetAvailableExaminerAsync(Guid candidateId);
        Task<IEnumerable<ExaminerPo>> GetAllExaminerAsync();
        Task ExaminerMatchedAsync(ExaminerPo examinerEntity, CandidatePo candidateEntity);
        Task ResetAsync();
    }
}
