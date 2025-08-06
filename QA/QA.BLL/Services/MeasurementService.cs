

using QA.BLL.Interfaces;
using QA.Domain.Models;

namespace QA.BLL.Services
{
    public class MeasurementService : IMeasurementService
    {
        public string GenerateOrderKey(string ShortenedOrderKey, string trialNo)
        {

            if (trialNo == null)
            {
               return ShortenedOrderKey;
            }
            else
            {
               return string.Concat(ShortenedOrderKey, "-", trialNo);
            }
        }
    }
}
