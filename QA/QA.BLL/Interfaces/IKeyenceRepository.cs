
using QA.Domain.Models.Keyence;

namespace QA.BLL.Interfaces
{
    public interface IKeyenceRepository
    {
        Task<List<KeyenceMeasurement>> GetMeasurementsBy(string orderKey, string productSymbol);
        Task<List<KeyenceParameter>> GetParametersBy(string pdmNo, string productSymbol);
        Task<KeyenceParameter> GetParameterWithMeasurementsBy(int ParameterId);

    }
}
