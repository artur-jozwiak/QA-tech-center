
namespace QA.BLL.Interfaces
{
    public interface IMeasurementService
    {
        string GenerateOrderKey(string shortenedOrderKey, string trialNo);
    }
}
