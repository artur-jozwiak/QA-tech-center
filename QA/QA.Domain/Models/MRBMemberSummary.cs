namespace QA.Domain.Models
{
    public  class MRBMemberSummary
    {
        public int Id { get; set; }
        public string? Member { get; set; }
        public string? Summary { get; set; }
        public bool Completed { get; set; }
        public bool NotificationReceived { get; set; }
        public DateTime? ModificationDate { get; set; }

        public virtual MRB MRB { get; set; }
        public int MRBId { get; set; }

        public List<int> MRBDipositions { get; set; } = new();
        public int PositionInQueue { get; set; }
    }
}
