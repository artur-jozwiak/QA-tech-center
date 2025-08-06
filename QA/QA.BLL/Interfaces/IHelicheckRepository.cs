
using QA.Domain.Models.Helicheck.Models;

namespace QA.BLL.Interfaces
{
    public interface IHelicheckRepository
    {
        List<HelicheckParameter> GetParametersBy(string orderNo);
        List<HelicheckResult> GetResultsBy(int parameterId, string orderNo);
        bool OrderHasHelicheckMeasurements(string orderNo);
    }
}
