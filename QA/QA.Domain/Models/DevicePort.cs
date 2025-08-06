namespace QA.Domain.Models
{
    public class DevicePort
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public MeasuringDevice MeasuringDevice { get; set; }
        public int MeasuringDeviceId { get; set; }

        public virtual ICollection<Parameter>? Parameters { get; set; }
    }
}
