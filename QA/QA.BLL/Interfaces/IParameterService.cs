using QA.Domain.Models;


namespace QA.BLL.Interfaces
{
    public interface IParameterService
    {
        bool IsMagneticParameter(Parameter parameter);
        bool ShowLaboratoryStats(Parameter parameter);
        bool IncludeParameterInReport(string parameterName);
        Task<string?> AssignSpecificationLimits(Operation operation);
    }
}
