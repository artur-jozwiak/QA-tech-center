using QA.BLL.Interfaces;
using System.Globalization;

namespace QA.BLL.Services
{
    public class SPCService : ISPCService
    {
        public decimal CalculateMin(List<decimal> values)
        {
            return Math.Round(values.Min(), 3);
        }

        public decimal CalculateMax(List<decimal> values)
        {
            return Math.Round(values.Max(), 3);
        }

        public decimal CalculateAvg(List<decimal> values)
        {
            return Math.Round(values.Average(), 3);
        }

        public decimal CalculateAvg(List<decimal?> nullableValues)
        {
            if(nullableValues.All(e => e == null))
            {
                return 0;
            }

            List<decimal> values = new();

            foreach (decimal? value in nullableValues)
            {
                if (value != null)
                {
                    values.Add((decimal)value);
                }
            }

            return Math.Round(values.Average(), 5);
        }

        public decimal CalculateStandardDeviation(List<decimal> values, decimal avg)
        {
            decimal standardDeviation = 0;

            if (values.Any())
            {
                double sum = values.Sum(e => Math.Pow((double)e - (double)avg, 2));
                double std = Math.Sqrt(sum / values.Count);
                standardDeviation = Math.Round((decimal)std, 5);
            }

            return standardDeviation;
        }

        public decimal CalculateDelta(decimal max, decimal min)
        {
            return Math.Round(max - min, 3);
        }

        public decimal CalculateCp(decimal usl, decimal lsl, decimal? standardDeviation)
        {
            if (standardDeviation != 0)
            {
                return Math.Round((usl - lsl) / (6 * (decimal)standardDeviation), 3);
            }
            else
            {
                return 0;
            }
        }

        public decimal CalculateCpk(decimal usl, decimal lsl, decimal avg, decimal standardDeviation)
        {
            if (standardDeviation != 0)
            {
                decimal cpu = (usl - avg) / (3 * standardDeviation);
                decimal cpl = (avg - lsl) / (3 * standardDeviation);
                return Math.Round(Math.Min(cpu, cpl), 3);
            }
            else
            {
                return 0;
            }
        }

        public bool IsOutsideTolerance(decimal minValue, decimal maxValue, decimal value)
        {
            //return value < minValue || value > maxValue;

            if(maxValue == 0 || minValue == 0)
                return false;

            return value < minValue || value > maxValue;
        }


        public decimal ConvertStringToDecimal(string strValue)
        {
            if (!decimal.TryParse(strValue, CultureInfo.InvariantCulture, out decimal value))
            {
                return 0;
            }

            return Math.Round(value, 3);
        }
    }
}
