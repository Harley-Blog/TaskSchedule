using BC.TS.Application.Dto;

namespace BC.TS.Application.Service.Facade
{
    public interface IDispatcherApplication
    {
        Task<IEnumerable<CandidateDto>> GenerateCandidateListAsync(int count);
        Task<IEnumerable<ExaminerDto>> GenerateExaminerListAsync(int count);
        Task<IEnumerable<ExaminerAppraiseDto>> AppraiseAsync();
        Task ResetAsync();
    }
}