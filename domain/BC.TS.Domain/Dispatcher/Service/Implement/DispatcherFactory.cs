using BC.TS.Domain.Dispatcher.Entity;
using BC.TS.Domain.Dispatcher.Service.Facade;

namespace BC.TS.Domain.Dispatcher.Service.Implement
{
    public class DispatcherFactory : IDispatcherFactory
    {
        private readonly Random random = new Random();
        private readonly int minCount = 2;
        private readonly int maxCount = 7;
        private readonly IEnumerable<string> _examinerNameList = "abcdefghigklmnopqrstuvwxyz".ToCharArray().Select(s => s.ToString());
        private readonly IEnumerable<string> _candidateNameList = "ABCDEFGHIJKLMNOPQRSTUVWXYG".ToCharArray().Select(s => s.ToString());

        /// <summary>
        /// Generate candidate list
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IEnumerable<Candidate>> GenerateCandidateListAsync(int count)
        {
            if (count <= 0 || count > _candidateNameList.Count())
            {
                throw new ArgumentException("Invalid parameter.", nameof(count));
            }

            var candidateNameList = new List<string>(_candidateNameList);
            var candidateList = new List<Candidate>();
            var index = count - 1;
            do
            {
                var i = random.Next(0, candidateNameList.Count());
                candidateList.Add(new Candidate(name: candidateNameList[i]));
                candidateNameList.RemoveAt(i);
            }
            while (index-- > 0);

            return await Task.FromResult(candidateList);
        }

        /// <summary>
        /// Generate examiner list
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Examiner>> GenerateExaminerListAsync(int count)
        {
            var exmainerNameList = new List<string>(_examinerNameList);
            if (count <= 0 || count > _examinerNameList.Count())
            {
                throw new ArgumentException("Invalid parameter.", nameof(count));
            }

            var examinerNameList = new List<string>(_examinerNameList);
            var examinerList = new List<Examiner>();
            var index = count - 1;
            do
            {
                var i = random.Next(0, examinerNameList.Count());
                examinerList.Add(new Examiner(name: examinerNameList[i], appraiseNumber: random.Next(minCount, maxCount)));
                examinerNameList.RemoveAt(i);
            }
            while (index-- > 0);

            return await Task.FromResult(examinerList.OrderBy(s => s.AppraiseNumber));
        }
    }
}
