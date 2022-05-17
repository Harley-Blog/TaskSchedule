namespace BC.TS.Domain.Dispatcher.Entity
{
    public class Candidate
    {
        private const int MaxAppraiseIndex = 2;
        /// <summary>
        /// Identity
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Candidate name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Appraise index
        /// </summary>
        public int AppraiseIndex { get; private set; }
        /// <summary>
        /// Is can be appraised
        /// </summary>
        public bool IsAppraisedOver { get; set; }

        /// <summary>
        /// ctor 
        /// </summary>
        public Candidate()
        {
            AppraiseIndex = 1;
        }

        /// <summary>
        /// ctor
        /// </summary>
        public Candidate(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            AppraiseIndex = 1;
        }

        /// <summary>
        /// The appraise has been carried out
        /// </summary>
        /// <returns></returns>
        public void MatchExaminer()
        {
            IsAppraisedOver = AppraiseIndex >= MaxAppraiseIndex;
            AppraiseIndex++;
        }
    }
}
