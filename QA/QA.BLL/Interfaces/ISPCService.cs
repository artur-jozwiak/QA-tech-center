using QA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.BLL.Interfaces
{
    public interface ISPCService
    {
        decimal CalculateMin(List<decimal> values);
        decimal CalculateMax(List<decimal> values);
        decimal CalculateAvg(List<decimal> values);
        decimal CalculateAvg(List<decimal?> values);

        decimal CalculateStandardDeviation(List<decimal> values, decimal avg);

        decimal CalculateDelta(decimal max, decimal min);
        decimal CalculateCp(decimal usl, decimal lsl, decimal? sigma);
        decimal CalculateCpk(decimal usl, decimal lsl, decimal avg, decimal sigma);
        //bool IsOutsideTolerance(Parameter parameter, decimal value);
        bool IsOutsideTolerance(decimal minValue, decimal maxValue, decimal value);


        decimal ConvertStringToDecimal(string strValue);
    }
}
