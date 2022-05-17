using BC.TS.Domain.Dispatcher.Entity;
using MediatR;

namespace BC.TS.Domain.Dispatcher.Command
{
    public class GenerateExaminerCommand : IRequest<IEnumerable<Examiner>>
    {
        public int Count { get; set; }
    }
}
