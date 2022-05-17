namespace BC.TS.Domain.Dispatcher.Repository.PersistenceObject
{
    public class CandidatePo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int AppraiseIndex { get; set; }
        public bool IsAppraisedOver { get; set; }
    }
}
