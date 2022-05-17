using AutoMapper;
using BC.TS.Application.Dto;
using BC.TS.Application.Service.Facade;
using BC.TS.Domain.Dispatcher.Command;
using BC.TS.Domain.Dispatcher.Repository.Facade;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BC.TS.Application.Service.Implement
{
    public class DispatcherApplication : IDispatcherApplication
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IDispatcherRepo _dispatcherRepo;
        private readonly ILogger<DispatcherApplication> _logger;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="mapper"></param>
        public DispatcherApplication(IMediator mediator,
            IMapper mapper,
            IDispatcherRepo dispatcherRepo, 
            ILogger<DispatcherApplication> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _dispatcherRepo = dispatcherRepo;
            _logger = logger;
        }

        /// <summary>
        /// Execute appraise
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ExaminerAppraiseDto>> AppraiseAsync()
        {
            _logger.LogInformation("Start matching");
            var command = new AppraiseCommand();
            var examinerList = await _mediator.Send(command);
            var result = examinerList.Select(s => new ExaminerAppraiseDto()
            {
                Result = s.GetMatchResult()
            });
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Generate candidate dto list
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CandidateDto>> GenerateCandidateListAsync(int count)
        {
            _logger.LogInformation("Generate candidate list");
            var command = new GenerateCandidateCommand()
            {
                Count = count
            };

            var candidateList = await _mediator.Send(command);
            return _mapper.Map<IEnumerable<CandidateDto>>(candidateList);
        }

        /// <summary>
        /// Generate examiner dto list
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ExaminerDto>> GenerateExaminerListAsync(int count)
        {
            _logger.LogInformation("Generate examiner list");
            var command = new GenerateExaminerCommand()
            {
                Count = count
            };

            var examinerList = await _mediator.Send(command);
            return _mapper.Map<IEnumerable<ExaminerDto>>(examinerList);
        }

        /// <summary>
        /// Reset the data store
        /// </summary>
        /// <returns></returns>
        public async Task ResetAsync()
        {
            _logger.LogInformation("Reset data");
            await _dispatcherRepo.ResetAsync();
        }
    }
}
