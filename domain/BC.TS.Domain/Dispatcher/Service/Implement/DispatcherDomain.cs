using AutoMapper;
using BC.TS.Domain.Dispatcher.Entity;
using BC.TS.Domain.Dispatcher.Repository.Facade;
using BC.TS.Domain.Dispatcher.Repository.PersistenceObject;
using BC.TS.Domain.Dispatcher.Service.Facade;

namespace BC.TS.Domain.Dispatcher.Service.Implement
{
    public class DispatcherDomain : IDispatcherDomain
    {
        private readonly IDispatcherRepo _dispatcherRepo;
        private readonly IMapper _mapper;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dispatcherRepo"></param>
        public DispatcherDomain(IDispatcherRepo dispatcherRepo,
            IMapper mapper)
        {
            _dispatcherRepo = dispatcherRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Examiner>> ExecuteAppraiseAsync()
        {
            while (true)
            {
                var candidatePo = await _dispatcherRepo.GetAvailableCandidateAsync();
                if (candidatePo == null)
                {
                    break;
                }
                var examinerPo = await _dispatcherRepo.GetAvailableExaminerAsync(candidatePo.Id);
                if (examinerPo is null)
                {
                    break;
                }

                var candidate = _mapper.Map<Candidate>(candidatePo);
                var examiner = _mapper.Map<Examiner>(examinerPo);
                examiner.MatchCandidate(candidate);
                candidate.MatchExaminer();

                candidatePo = _mapper.Map<CandidatePo>(candidate);
                examinerPo = _mapper.Map<ExaminerPo>(examiner);
                examinerPo.CandidatesIndex[candidatePo.Id] = examiner.CandidatesIndex[candidatePo.Id];

                await _dispatcherRepo.ExaminerMatchedAsync(examinerPo, candidatePo);
            }

            var examinerPoList = await _dispatcherRepo.GetAllExaminerAsync();
            var examinerDoList = _mapper.Map<IEnumerable<Examiner>>(examinerPoList);
            foreach (var item in examinerDoList)
            {
                item.CandidatesIndex = examinerPoList.First(s => s.Id == item.Id).CandidatesIndex;
            }

            return examinerDoList;
        }
    }
}
