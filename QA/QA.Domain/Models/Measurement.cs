namespace QA.Domain.Models
{
    public class Measurement
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public string? Operator { get; set; }
        public DateTime Date { get; set; }
        public bool IsControllerMeasuremnt { get; set; }
        public int? EdgeNo {  get; set; }// to wykorzystac do strona dla powłoki?
        public string OrderKey { get; set; }
        public virtual Parameter Parameter { get; set; }
        public int ParameterId { get; set; }
        public virtual Order? Order { get; set; }
        public int? OrderId { get; set; }
        public int? MeasurementSeriesId { get; set; }
        public MeasurementsSeries? MeasurementsSeries { get; set; }
    }
}
