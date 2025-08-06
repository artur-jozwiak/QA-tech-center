using System.ComponentModel.DataAnnotations;
using QA.Domain.Models.Enums;

namespace QA.Domain.Models
{
    public class Parameter
    {
        private decimal _lsl;
        private decimal _usl;

        public int Id { get; set; }

        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; } = string.Empty;
        public string? Unit { get; set; }

        public decimal LSL
        {
            get {
                if(LowerTolerance != 0)
                {
                    return NominalValue + LowerTolerance;
                }
                else
                {
                    //return NominalValue;

                    //return _lsl;
                    if (_lsl == 0)
                    {
                        return NominalValue;
                    }
                    else
                    {
                        return _lsl;
                    }

                }
            }
            set {
                if(LowerTolerance != 0)
                {
                    LowerTolerance = value - NominalValue;
                }
                else
                {
                    _lsl = value;
                }
            }
        }

        public decimal USL
        {
            get
            {
                if (UpperTolerance != 0)
                {
                    return NominalValue + UpperTolerance;
                }
                else
                {
                    //return NominalValue; to jest na produkcji(Zle w laboratorium bo brak tolerancji)
                    //return _usl;to było

                    if (_usl == 0)
                    {
                        return NominalValue;
                    }
                    else
                    {
                        return _usl;
                    }
                }
            }
            set
            {
                if (UpperTolerance != 0)
                {
                    UpperTolerance = value - NominalValue;
                }
                else
                {
                    _usl = value;
                }
            }
        }


        //public decimal USL
        //{
        //    get => NominalValue + UpperTolerance;
        //    set => UpperTolerance = value - NominalValue;
        //}


        public decimal LowerTolerance { get; set; }
        public decimal UpperTolerance { get; set; }

        public decimal NominalValue { get; set; }

        [Display(Name = "Klasa próby")]

        [StringLength(1, ErrorMessage = "Max 1 znak")]
        public string? SampleClass { get; set; }
        public string? Comment { get; set; }
        public bool AutomaticMeasurement { get; set; }
        public bool IsTrialParameter { get; set; }
        //public DateTime CreationDate { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual Operation Operation { get; set; }
        public int OperationId { get; set; }
        public virtual ICollection<Measurement> Measurements { get; set; } = new List<Measurement>();

        public DevicePort? DevicePort { get; set; }
        public int? DevicePortId { get; set; }

        public virtual Image? Image { get; set; }
        public int? ImageId { get; set; }

        public ParameterType ParameterType { get; set; }
        public List<ChildParametersAssignement>? ChildParametersAssignements { get; set; }

        public bool IsChildParameter { get; set; }

        public int? EdgesQty { get; set; }
    }
}
