namespace BC.TS.Domain.Dispatcher.Repository.PersistenceObject
{
    public class ExaminerPo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int AppraiseNumber { get; set; }
        public List<CandidatePo> Candidates { get; set; } = new List<CandidatePo>();
        public Dictionary<Guid, int> CandidatesIndex { get; set; } = new Dictionary<Guid, int>();
    }
}
