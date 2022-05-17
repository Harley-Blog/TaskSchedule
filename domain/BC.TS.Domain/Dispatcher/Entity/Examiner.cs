namespace BC.TS.Domain.Dispatcher.Entity
{
    public class Examiner
    {
        /// <summary>
        /// Identity
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Examiner name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Appraise number
        /// </summary>
        public int AppraiseNumber { get; set; }
        /// <summary>
        /// Candidates appraised
        /// </summary>
        public List<Candidate> Candidates { get; init; }
        /// <summary>
        /// Candidate appraise index
        /// </summary>
        public Dictionary<Guid, int> CandidatesIndex { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public Examiner()
        {
            Candidates = new List<Candidate>();
            CandidatesIndex = new Dictionary<Guid, int>();
        }

        /// <summary>
        /// ctor
        /// </summary>
        public Examiner(string name, int appraiseNumber)
        {
            Id = Guid.NewGuid();
            Name = name;
            AppraiseNumber = appraiseNumber;
            Candidates = new List<Candidate>();
            CandidatesIndex = new Dictionary<Guid, int>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidate"></param>
        public void MatchCandidate(Candidate candidate)
        {
            Candidates.Add(candidate);
            CandidatesIndex.Add(candidate.Id, candidate.AppraiseIndex);
        }

        /// <summary>
        /// Get appraise result
        /// </summary>
        /// <returns></returns>
        public string GetMatchResult()
        {
            var candidateString = Candidates.OrderBy(s => s.Name).Select(s => $"{s.Name}{CandidatesIndex[s.Id]}");
            return $"考官 {Name} 判 {string.Join("/", candidateString)}";
        }
    }
}
