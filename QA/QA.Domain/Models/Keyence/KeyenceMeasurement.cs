
namespace QA.Domain.Models.Keyence
{
    //skorzystać z tabeli measurements?
    //Czy powiazac z order?
    //Czy 
    public class KeyenceMeasurement
    {
        public Guid Id { get; set; }
        public string OrderNo { get; set; }
        public int Series {  get; set; }
        public int Number {  get; set; }
        public decimal Value { get; set; }

        public string FileName { get; set; }

        public DateTime FileModificationDate { get; set; } 
        public DateTime Date { get; set; }


        public virtual KeyenceParameter Parameter { get; set; }
        public int ParameterId { get; set; }

    }
}
