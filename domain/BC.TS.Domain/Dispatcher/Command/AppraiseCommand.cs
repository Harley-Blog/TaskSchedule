using BC.TS.Domain.Dispatcher.Entity;
using MediatR;

namespace BC.TS.Domain.Dispatcher.Command
{
    public class AppraiseCommand : IRequest<IEnumerable<Examiner>>
    {
    }
}
