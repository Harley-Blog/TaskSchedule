using BC.TS.Domain.Dispatcher.Repository.Facade;
using BC.TS.Domain.Dispatcher.Repository.PersistenceObject;
using System.Collections.Concurrent;

namespace BC.TS.Repository
{
    public class DispatcherRepo : IDispatcherRepo
    {
        private static readonly ConcurrentDictionary<Guid, CandidatePo> _candidateStore = new ConcurrentDictionary<Guid, CandidatePo>();
        private static readonly ConcurrentDictionary<Guid, ExaminerPo> _examinerStore = new ConcurrentDictionary<Guid, ExaminerPo>();
        public DispatcherRepo()
        { }

        public async Task AddCandidatesAsync(IEnumerable<CandidatePo> entities)
        {
            foreach (var entity in entities)
            {
                if (!_candidateStore.ContainsKey(entity.Id))
                {
                    _candidateStore.TryAdd(entity.Id, entity);
                }
            }
            await Task.CompletedTask;
        }

        public async Task AddExaminersAsync(IEnumerable<ExaminerPo> entities)
        {
            foreach (var entity in entities)
            {
                if (!_examinerStore.ContainsKey(entity.Id))
                {
                    _examinerStore.TryAdd(entity.Id, entity);
                }
            }
            await Task.CompletedTask;
        }

        public async Task<CandidatePo?> GetAvailableCandidateAsync()
        {
            var candidatePo = _candidateStore.Where(s => !s.Value.IsAppraisedOver)
                .OrderBy(s => Guid.NewGuid())
                .Select(s => s.Value)
                .FirstOrDefault();

            return await Task.FromResult(candidatePo);
        }

        public async Task<ExaminerPo?> GetAvailableExaminerAsync(Guid candidateId)
        {
            var examinerPo = _examinerStore
                .Where(s => !s.Value.Candidates.Any(c => c.Id == candidateId)
                    && s.Value.AppraiseNumber > s.Value.Candidates.Count())
                .OrderBy(s => s.Value.AppraiseNumber - s.Value.Candidates.Count())
                .Select(s => s.Value)
                .LastOrDefault();

            return await Task.FromResult(examinerPo);
        }

        public async Task ExaminerMatchedAsync(ExaminerPo examinerEntity, CandidatePo candidateEntity)
        {
            var examiner = _examinerStore.Where(s => s.Key == examinerEntity.Id)
                .Select(s => s.Value)
                .FirstOrDefault();

            var candidate = _candidateStore.Where(s => s.Key == candidateEntity.Id)
                .Select(s => s.Value)
                .FirstOrDefault();

            if (examiner == null || candidate == null)
            {
                return;
            }

            examiner.CandidatesIndex[candidateEntity.Id] = examinerEntity.CandidatesIndex[candidateEntity.Id];
            examiner.Candidates.Add(new CandidatePo()
            {
                Id = candidateEntity.Id,
                AppraiseIndex = candidateEntity.AppraiseIndex,
                IsAppraisedOver = candidateEntity.IsAppraisedOver,
                Name = candidateEntity.Name
            });

            candidate.IsAppraisedOver = candidateEntity.IsAppraisedOver;
            candidate.AppraiseIndex = candidateEntity.AppraiseIndex;
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<ExaminerPo>> GetAllExaminerAsync()
        {
            return await Task.FromResult(_examinerStore.Values.OrderBy(s => s.AppraiseNumber));
        }

        public async Task ResetAsync()
        {
            _examinerStore.Clear();
            _candidateStore.Clear();
            await Task.CompletedTask;
        }
    }
}