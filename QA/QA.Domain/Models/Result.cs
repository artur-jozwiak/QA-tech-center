namespace QA.Domain.Models
{
    public class Result
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public string? Operator { get; set; }
        public int Series {  get; set; }
        public DateTime Date { get; set; }
        public string OrderKey { get; set; }
        public virtual DescriptiveParameter Parameter { get; set; }
        public int ParameterId { get; set; }
        public virtual Order? Order { get; set; }
        public int? OrderId { get; set; }
    }
}
