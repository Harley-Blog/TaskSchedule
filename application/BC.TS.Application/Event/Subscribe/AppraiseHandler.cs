using BC.TS.Domain.Dispatcher.Command;
using BC.TS.Domain.Dispatcher.Entity;
using BC.TS.Domain.Dispatcher.Service.Facade;
using MediatR;

namespace BC.TS.Application.Event.Subscribe
{
    public class AppraiseHandler : IRequestHandler<AppraiseCommand, IEnumerable<Examiner>>
    {
        private readonly IDispatcherDomain _dispatcherDomain;
        public AppraiseHandler(IDispatcherDomain dispatcherDomain)
        {
            _dispatcherDomain = dispatcherDomain;
        }
        public async Task<IEnumerable<Examiner>> Handle(AppraiseCommand request, CancellationToken cancellationToken)
        {
            var result = await _dispatcherDomain.ExecuteAppraiseAsync();
            return result;
        }
    }
}
