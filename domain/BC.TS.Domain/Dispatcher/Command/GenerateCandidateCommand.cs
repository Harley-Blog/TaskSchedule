using BC.TS.Domain.Dispatcher.Entity;
using MediatR;

namespace BC.TS.Domain.Dispatcher.Command
{
    public class GenerateCandidateCommand : IRequest<IEnumerable<Candidate>>
    {
        public int Count { get; set; }
    }
}
