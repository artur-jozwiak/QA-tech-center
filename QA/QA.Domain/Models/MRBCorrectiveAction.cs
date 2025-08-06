namespace QA.Domain.Models
{
    public class MRBCorrectiveAction
    {
        public int Id { get; set; }
        public string? Creator { get; set; }
        public string? Action { get; set; }
        public string? StaffResponsible { get; set; }
        public DateTime? DueDate { get; set; }
        public virtual MRB MRB { get; set; }
        public int MRBId { get; set; }
    }
}
