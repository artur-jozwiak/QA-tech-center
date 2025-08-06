using QA.Domain.Models;

namespace QA.BLL.Interfaces
{
    public interface IMeasurementSeriesRepository
    {
        void Delete(MeasurementsSeries measurementsSeries);
        Task AddRangeAsync(List<MeasurementsSeries> measurementSeriesList);

        List<MeasurementsSeries> GetMeasurementSeriesesWithoutOrderAssigned();
    }
}
