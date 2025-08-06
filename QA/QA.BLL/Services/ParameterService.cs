using Microsoft.CodeAnalysis;
using QA.BLL.Interfaces;
using QA.Domain.Models;

namespace QA.BLL.Services
{
    public class ParameterService : IParameterService
    {
        //private readonly IPowdersRepository _powdersRepository;

        private readonly IUnitOfWork _unitOfWork;
        private readonly string[] _magneticParametersNames = { "HC", "σ", "MS", "Waga" };
        private readonly string[] _laboratoryStatsParameter = { "HC", "MS", "Gęstość" };
        private readonly string[] _reportExcludedParameters = { "Wąs", "K1C", "Twardość", "Waga", "σs" };

        public ParameterService(IUnitOfWork unitOfWork)
        {
                _unitOfWork = unitOfWork;
        }

        public bool IsMagneticParameter(Parameter parameter)
        {
            return _magneticParametersNames.Any(x => parameter.Name.Contains(x));
        }

        public bool ShowLaboratoryStats(Parameter parameter)
        {
            return _laboratoryStatsParameter.Any(x => parameter.Name.Contains(x));
        }

        public bool IncludeParameterInReport(string parameterName)
        {
            return !_reportExcludedParameters.Any(x => parameterName.Contains(x));
        }

        public async Task<string?> AssignSpecificationLimits(Operation operation)
        {
            List<Parameter> prametersforUpdate = new();
            string powderName = operation.Product.Description.Trim().Split(" ").Last();
            var powder = await _unitOfWork.Powder.GetBy(powderName);

            if (powder == null)
            {
                return $"Proszek o nazwie: {powderName} nie istnieje.";
            }

            var hcjParameter = operation.Parameters.First(p => p.Name.Contains("HC"));
            var msParameter = operation.Parameters.First(p => p.Name.Contains("MS"));
            var densityParameter = operation.Parameters.First(p => p.Name.Contains("Gęstość"));
            var hardnesParameter = operation.Parameters.First(p => p.Name.Contains("Twardość"));

            if ((hcjParameter.LSL == 0 && powder.HCJMin != null) ||
                 (hcjParameter.NominalValue == 0 && powder.HCJNominal != null) ||
                 (hcjParameter.USL == 0 && powder.HCJMax != null))
            {
                hcjParameter.LSL = (decimal)(powder.HCJMin ?? 0);
                hcjParameter.NominalValue = (decimal)(powder.HCJNominal ?? 0);
                hcjParameter.USL = (decimal)(powder.HCJMax ?? 0);
                prametersforUpdate.Add(hcjParameter);
            }

            if ((msParameter.LSL == 0 && powder.COMMin != null) ||
                (msParameter.NominalValue == 0 && powder.COMNominal != null) ||
                (msParameter.USL == 0 && powder.COMMax != null))
            {
                msParameter.LSL = (decimal)(powder.COMMin ?? 0);
                msParameter.NominalValue = (decimal)(powder.COMNominal ?? 0);
                msParameter.USL = (decimal)(powder.COMMax ?? 0);
                prametersforUpdate.Add(msParameter);
            }

            if ((densityParameter.LSL == 0 && powder.DensityMin != null) ||
                (densityParameter.NominalValue == 0 && powder.DensityNominal != null) ||
                (densityParameter.USL == 0 && powder.DensityMax != null))
            {
                densityParameter.LSL = (decimal)(powder.DensityMin ?? 0);
                densityParameter.NominalValue = (decimal)(powder.DensityNominal ?? 0);
                densityParameter.USL = (decimal)(powder.DensityMax ?? 0);
                prametersforUpdate.Add(densityParameter);
            }

            if ((hardnesParameter.LSL == 0 && powder.HV30Min != null) ||
                (hardnesParameter.NominalValue == 0 && powder.HV30Nominal != null) ||
                (hardnesParameter.USL == 0 && powder.HV30Max != null))
            {
                hardnesParameter.LSL = (decimal)(powder.HV30Min ?? 0);
                hardnesParameter.NominalValue = (decimal)(powder.HV30Nominal ?? 0);
                hardnesParameter.USL = (decimal)(powder.HV30Max ?? 0);
                prametersforUpdate.Add(hardnesParameter);
            }

            if (prametersforUpdate.Count != 0)
            {
                _unitOfWork.Parameter.UpdateRange(prametersforUpdate);
                await _unitOfWork.CompleteAsync();
            }

            return null;
        }

    }
}
