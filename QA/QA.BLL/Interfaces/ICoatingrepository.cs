
using QA.Domain.Models.CoatingModels;

namespace QA.BLL.Interfaces
{
    public interface ICoatingRepository
    {
        CoatingProcess? GetBy(int processNo);
        Coating GetBy(string coating);
        Coating GetCoatingBy(int id);
        void Add(CoatingProcess coatingProcess);
        void Add(Coating coatingSpecification);
        void Add(CoatingMeasurementSeries measurementSeries);
        List<Coating> GetAll();
        List<CoatingProcess> GetAllProcesses();
        Task<List<CoatingProcess>> GetAllProcessesAsync();

        List<CoatingMeasurementSeries> GetProcessMeasurementsBy(int processNo);
        List<CoatingMeasurementSeries>? GetProcessMeasurementsBy(string procesSymbol);
        List<CoatingMeasurementSeries>? GetCoatingMeasurementsBy(string coatingName);
        void RemoveMeasurement(CoatingMeasurementSeries measurementSeries);
        void RemoveCoating(Coating coating);
    }
}
