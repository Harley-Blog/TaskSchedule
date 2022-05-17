using AutoMapper;
using BC.TS.Domain.Dispatcher.Command;
using BC.TS.Domain.Dispatcher.Entity;
using BC.TS.Domain.Dispatcher.Repository.Facade;
using BC.TS.Domain.Dispatcher.Repository.PersistenceObject;
using BC.TS.Domain.Dispatcher.Service.Facade;
using MediatR;

namespace BC.TS.Application.Event.Subscribe
{
    public class GenerateCandidateHandler : IRequestHandler<GenerateCandidateCommand, IEnumerable<Candidate>>
    {
        private readonly IDispatcherFactory _dispatcherFactory;
        private readonly IMapper _mapper;
        private readonly IDispatcherRepo _dispatcherRepo;
        public GenerateCandidateHandler(IDispatcherFactory dispatcherFactory,
            IMapper mapper,
            IDispatcherRepo dispatcherRepo)
        {
            _dispatcherFactory = dispatcherFactory;
            _mapper = mapper;
            _dispatcherRepo = dispatcherRepo;
        }
        public async Task<IEnumerable<Candidate>> Handle(GenerateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidateList = await _dispatcherFactory.GenerateCandidateListAsync(request.Count);
            var candidatePoList = _mapper.Map<IEnumerable<CandidatePo>>(candidateList);
            await _dispatcherRepo.AddCandidatesAsync(candidatePoList);
            return candidateList;
        }
    }
}
