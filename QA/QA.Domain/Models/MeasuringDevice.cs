using System.ComponentModel.DataAnnotations;

namespace QA.Domain.Models
{
    public class MeasuringDevice
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Numer seryjny jest wymagany.")]
        public string SerialNo { get; set; }

        [Required(ErrorMessage = "Jedno wejście jest wymagane.")]
        public ICollection<DevicePort> Ports { get; set; }

        public DateTime CalibrationDate { get; set; } 

        //public TimeSpan CalibrationPeriod { get; set; }
        //public DateTime NextCalibrationDate
        //{
        //    get
        //    {
        //        // Dodaj aktualną datę kalibracji do okresu czasu
        //        return CalibrationDate.Add(CalibrationPeriod);
        //    }
        //}
    }
}
