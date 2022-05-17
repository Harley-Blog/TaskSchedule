using AutoMapper;
using BC.TS.Domain.Dispatcher.Command;
using BC.TS.Domain.Dispatcher.Entity;
using BC.TS.Domain.Dispatcher.Repository.Facade;
using BC.TS.Domain.Dispatcher.Repository.PersistenceObject;
using BC.TS.Domain.Dispatcher.Service.Facade;
using MediatR;

namespace BC.TS.Application.Event.Subscribe
{
    public class GenerateExaminerHandler : IRequestHandler<GenerateExaminerCommand, IEnumerable<Examiner>>
    {

        private readonly IDispatcherFactory _dispatcherFactory;
        private readonly IMapper _mapper;
        private readonly IDispatcherRepo _dispatcherRepo;
        public GenerateExaminerHandler(IDispatcherFactory dispatcherFactory,
            IMapper mapper,
            IDispatcherRepo dispatcherRepo)
        {
            _dispatcherFactory = dispatcherFactory;
            _mapper = mapper;
            _dispatcherRepo = dispatcherRepo;
        }
        public async Task<IEnumerable<Examiner>> Handle(GenerateExaminerCommand request, CancellationToken cancellationToken)
        {
            var examinerList = await _dispatcherFactory.GenerateExaminerListAsync(request.Count);
            var examinerPoList = _mapper.Map<IEnumerable<ExaminerPo>>(examinerList);
            await _dispatcherRepo.AddExaminersAsync(examinerPoList);
            return examinerList;
        }
    }
}
