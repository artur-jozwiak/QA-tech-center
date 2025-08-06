using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Configuration;
using QA.BLL.Interfaces;
using QA.BLL.Mappings;
using QA.BLL.Models;
using QA.Domain.Models;
using System.Globalization;

namespace QA.DataAccess.Repositories
{
    public class LaboratoryRepository : ILaboratoryRepository
    {
        private IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IParameterService _parameterService;

        public LaboratoryRepository(IConfiguration configuration,IUnitOfWork unitOfWork, IParameterService parameterService)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _parameterService = parameterService;
        }


        public async Task<string?> ReadMeasurementsForOrder(Order order, string? trial, List<Parameter> parameters, string user)
        {
            var operation = parameters.FirstOrDefault().Operation;
            var measurementSeriesList = new List<MeasurementsSeries>();

            string path = _configuration["AppSettings:LaboratoryPath"];

            string orderKey = trial != null ? string.Concat(order.ShortenedKey, "-", trial) : order.ShortenedKey;

            string msPath = Path.Combine(path, "MS", orderKey + ".csv");
            string hcjPath = Path.Combine(path, "HCJ", orderKey + ".csv");

            var hcList = new List<HCJLaboratoryModel>();
            var msList = new List<MSLaboratoryModel>();

            bool hcjFileExists = File.Exists(hcjPath);
            bool msFileExists = File.Exists(msPath);

            using (var msReader = new StreamReader(msPath))
            using (var msCsv = new CsvReader(msReader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = "\t",
                HasHeaderRecord = false,
            }))
            {
                msCsv.Context.RegisterClassMap<MSLaboratoryModelMap>();

                while (msCsv.Read())
                {
                    var ms = msCsv.GetRecord<MSLaboratoryModel>();
                    msList.Add(ms);
                }
            }

            if (hcjFileExists)
            {
                using (var hcjReader = new StreamReader(hcjPath))
                using (var hcjCsv = new CsvReader(hcjReader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = "\t",
                    HasHeaderRecord = false,
                }))
                {
                    hcjCsv.Context.RegisterClassMap<HCJLaboratoryModelMap>();

                    while (hcjCsv.Read())
                    {
                        var hcj = hcjCsv.GetRecord<HCJLaboratoryModel>();
                        hcList.Add(hcj);
                    }
                }
            }

            int seriesCount = Math.Max(hcList.Count, msList.Count);

            for (int i = 0; i < seriesCount; i++)
            {
                MeasurementsSeries measurementSeries = new MeasurementsSeries
                {
                    Operation = operation,
                    OrderId = order.Id,
                    Index = i + 1,
                    Measurements = new List<Measurement>
            {
                new Measurement
                {
                    OrderId  = order.Id,
                    OrderKey = orderKey,
                    Parameter = parameters.FirstOrDefault(p => p.Name.Contains("HC")),
                    Operator =  user,
                    IsControllerMeasuremnt = true,
                    Value = hcjFileExists && i < hcList.Count ? hcList[i].HCJ : 0,
                    Date =  hcjFileExists && i < hcList.Count ? hcList[i].TimeStamp : DateTime.Now
                },
                new Measurement
                {
                    OrderId  = order.Id,
                    OrderKey = orderKey,
                    Parameter = parameters.FirstOrDefault(p => p.Name.Contains("σ")),
                    Operator =  user,
                    IsControllerMeasuremnt = true,
                    Value = msFileExists && i < msList.Count ? msList[i].Sigma : 0,
                    Date =  msFileExists && i < msList.Count ? msList[i].TimeStamp : DateTime.Now
                },
                new Measurement
                {
                    OrderId  = order.Id,
                    OrderKey = orderKey,
                    Parameter = parameters.FirstOrDefault(p => p.Name.Contains("MS")),
                    Operator =  user,
                    IsControllerMeasuremnt = true,
                    Value =  msList[i].MS,
                    Date = msList[i].TimeStamp
                },
                new Measurement
                {
                    OrderId  = order.Id,
                    OrderKey = orderKey,
                    Parameter = parameters.FirstOrDefault(p => p.Name.Contains("Waga")),
                    Operator =  user,
                    IsControllerMeasuremnt = true,
                    Value = msList[i].Weight,
                    Date = msList[i].TimeStamp
                },
                new Measurement
                {
                    OrderId  = order.Id,
                    OrderKey = orderKey,
                    Parameter = parameters.FirstOrDefault(p => p.Name.Contains("Gęstość")),
                    Operator =  user,
                    IsControllerMeasuremnt = true,
                    Value = 0,
                    Date = DateTime.Now
                },
                new Measurement
                {
                    OrderId  = order.Id,
                    OrderKey = orderKey,
                    Parameter = parameters.FirstOrDefault(p => p.Name.Contains("Twardość")),
                    Operator =  user,
                    IsControllerMeasuremnt = true,
                    Value = 0,
                    Date = DateTime.Now
                },
                new Measurement
                {
                    OrderId  = order.Id,
                    OrderKey = orderKey,
                    Parameter = parameters.FirstOrDefault(p => p.Name.Contains("Wąs 1")),
                    Operator =  user,
                    IsControllerMeasuremnt = true,
                    Value = 0,
                    Date = DateTime.Now
                },
                new Measurement
                {
                    OrderId  = order.Id,
                    OrderKey = orderKey,
                    Parameter = parameters.FirstOrDefault(p => p.Name.Contains("Wąs 2")),
                    Operator =  user,
                    IsControllerMeasuremnt = true,
                    Value = 0,
                    Date = DateTime.Now
                },
                new Measurement
                {
                    OrderId  = order.Id,
                    OrderKey = orderKey,
                    Parameter = parameters.FirstOrDefault(p => p.Name.Contains("Wąs 3")),
                    Operator =  user,
                    IsControllerMeasuremnt = true,
                    Value = 0,
                    Date = DateTime.Now
                },
                new Measurement
                {
                    OrderId  = order.Id,
                    OrderKey = orderKey,
                    Parameter = parameters.FirstOrDefault(p => p.Name.Contains("Wąs 4")),
                    Operator =  user,
                    IsControllerMeasuremnt = true,
                    Value = 0,
                    Date = DateTime.Now
                },
                new Measurement
                {
                    OrderId  = order.Id,
                    OrderKey = orderKey,
                    Parameter = parameters.FirstOrDefault(p => p.Name.Contains("K1C")),
                    Operator =  user,
                    IsControllerMeasuremnt = true,
                    Value = 0,
                    Date = DateTime.Now
                },
            }
                };

                measurementSeriesList.Add(measurementSeries);
            }

            try
            {
                await _unitOfWork.MeasurementSeries.AddRangeAsync(measurementSeriesList);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception)
            {
                throw;
            }
            await _parameterService.AssignSpecificationLimits(operation);

            return null;
        }


        // tylko do pobierania danych historycznych z lab
        public async Task<string?> ReadMeasurementsForMaster(string sinteringNo, string? user)
        {
            int laboratoryMasterOperationId = int.Parse(_configuration["AppSettings:LaboratoryMasterOperationId"]);
            var operation = await _unitOfWork.Operation.GetMasterControlOperationWithParameters(laboratoryMasterOperationId);

            List<Parameter> parameters = operation.Parameters.ToList();
            var measurementSeriesList = new List<MeasurementsSeries>();

            string path = _configuration["AppSettings:LaboratoryPath"];
            string msPath = Path.Combine(path, "MS", sinteringNo + ".csv");
            string hcjPath = Path.Combine(path, "HCJ", sinteringNo + ".csv");

            bool hcjFileExists = File.Exists(hcjPath);
            bool msFileExists = File.Exists(msPath);
            if (!msFileExists)
            {
                return $"Plik nie istnieje: {msPath}";
            }

            if (!hcjFileExists)
            {
                return $"Plik nie istnieje: {hcjPath}";
            }

            using (var msReader = new StreamReader(msPath))
            using (var msCsv = new CsvReader(msReader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = "\t",
                HasHeaderRecord = false,
            }))

            using (var hcjReader = new StreamReader(hcjPath))
            using (var hcjCsv = new CsvReader(hcjReader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = "\t",
                HasHeaderRecord = false,
            }))
            {
                hcjCsv.Context.RegisterClassMap<HCJLaboratoryModelMap>();
                msCsv.Context.RegisterClassMap<MSLaboratoryModelMap>();

                var msList = new List<MSLaboratoryModel>();
                while (msCsv.Read())
                {
                    var ms = msCsv.GetRecord<MSLaboratoryModel>();
                    msList.Add(ms);
                }

                var hcList = new List<HCJLaboratoryModel>();

                while (hcjCsv.Read())
                {
                    var hcj = hcjCsv.GetRecord<HCJLaboratoryModel>();
                    hcList.Add(hcj);
                }

                int seriesCount = Math.Max(hcList.Count, msList.Count);

                for (int i = 0; i < seriesCount; i++)
                {
                    MeasurementsSeries measurementSeries = new MeasurementsSeries();
                    measurementSeries.Operation = operation;
                    measurementSeries.Index = i + 1;

                    measurementSeries.Measurements = new()
                            {
                                new Measurement
                                {
                                    OrderKey = sinteringNo  + "-" + measurementSeries.Index,
                                    Parameter = parameters.FirstOrDefault(p => p.Name.Contains("HC")),
                                    Operator =  user,
                                    IsControllerMeasuremnt = true,
                                    Value = hcjFileExists && i < hcList.Count ? hcList[i].HCJ : 0,
                                    Date =  hcjFileExists && i < hcList.Count ? hcList[i].TimeStamp : DateTime.Now
                                },
                                new Measurement
                                {
                                    OrderKey = sinteringNo + "-" + measurementSeries.Index,
                                    Parameter = parameters.FirstOrDefault(p => p.Name.Contains("MS")),
                                    Operator =  user,
                                    IsControllerMeasuremnt = true,
                                    Value = msFileExists && i < msList.Count ? msList[i].MS : 0,
                                    Date =  msFileExists && i < msList.Count ? msList[i].TimeStamp : DateTime.Now
                                },
                                new Measurement
                                {
                                    OrderKey = sinteringNo  + "-" + measurementSeries.Index,
                                    Parameter = parameters.FirstOrDefault(p => p.Name.Contains("Waga")),
                                    Operator =  user,
                                    IsControllerMeasuremnt = true,
                                    Value = msFileExists && i < msList.Count ? msList[i].Weight : 0,
                                    Date =  msFileExists && i < msList.Count ? msList[i].TimeStamp : DateTime.Now
                                },
                            };
                    measurementSeriesList.Add(measurementSeries);
                }
                try
                {
                    await _unitOfWork.MeasurementSeries.AddRangeAsync(measurementSeriesList);
                    await _unitOfWork.CompleteAsync();
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return null;
        }

        public async Task<string?> ReadMeasurementsForOrder(string orderKey, int productId)
        {
            var measurementSeriesList = new List<MeasurementsSeries>();
            var operation = await _unitOfWork.Operation.GetLaboratoryOperationBy(productId);

            if(operation.Parameters.Any())
            {
                if (operation.Parameters.Any(p => p.Measurements.Any(m => m.OrderKey.Contains(orderKey))))
                {
                    return $"Dane dla zlecenia: {orderKey} zostały pobrane";
                }
            }

            List<Parameter> parameters = operation.Parameters.ToList();
            var hcParameter = parameters.FirstOrDefault(p => p.Name.Contains("HC"));
            var msParameter = parameters.FirstOrDefault(p => p.Name.Contains("MS"));


            string path = _configuration["AppSettings:LaboratoryPath"];
            string msPath = Path.Combine(path, "MS", orderKey + ".csv");
            string hcjPath = Path.Combine(path, "HCJ", orderKey + ".csv");

            bool hcjFileExists = File.Exists(hcjPath);
            bool msFileExists = File.Exists(msPath);

            if (!msFileExists)
            {
                return $"Plik nie istnieje: {msPath}";
            }

            if (!hcjFileExists)
            {
                return $"Plik nie istnieje: {hcjPath}";
            }
            try//
            {//


            using (var msReader = new StreamReader(msPath))
            using (var msCsv = new CsvReader(msReader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = "\t",
                HasHeaderRecord = false,
            }))

            using (var hcjReader = new StreamReader(hcjPath))
            using (var hcjCsv = new CsvReader(hcjReader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = "\t",
                HasHeaderRecord = false,
            }))
            {
                hcjCsv.Context.RegisterClassMap<HCJLaboratoryModelMap>();
                msCsv.Context.RegisterClassMap<MSLaboratoryModelMap>();

                var msList = new List<MSLaboratoryModel>();
                while (msCsv.Read())
                {
                    var ms = msCsv.GetRecord<MSLaboratoryModel>();
                    msList.Add(ms);
                }

                var hcList = new List<HCJLaboratoryModel>();

                while (hcjCsv.Read())
                {
                    var hcj = hcjCsv.GetRecord<HCJLaboratoryModel>();
                    hcList.Add(hcj);
                }

                int seriesCount = Math.Max(hcList.Count, msList.Count);

                for (int i = 0; i < seriesCount; i++)
                {
                    MeasurementsSeries measurementSeries = new MeasurementsSeries();
                    measurementSeries.Operation = operation;
                    measurementSeries.Index = i + 1;

                    measurementSeries.Measurements = new()
                    {
                                new Measurement
                                {
                                    OrderKey = orderKey,
                                    Parameter = hcParameter,
                                    Operator =  "Auto",
                                    IsControllerMeasuremnt = true,
                                    Value = hcjFileExists && i < hcList.Count ? hcList[i].HCJ : 0,
                                    Date =  hcjFileExists && i < hcList.Count ? hcList[i].TimeStamp : DateTime.Now
                                },
                                new Measurement
                                {
                                    OrderKey = orderKey,
                                    Parameter = msParameter,
                                    Operator =  "Auto",
                                    IsControllerMeasuremnt = true,
                                    Value = msFileExists && i < msList.Count ? msList[i].MS : 0,
                                    Date =  msFileExists && i < msList.Count ? msList[i].TimeStamp : DateTime.Now
                                },
                    };
                    measurementSeriesList.Add(measurementSeries);
                }
                try
                {
                    await _unitOfWork.MeasurementSeries.AddRangeAsync(measurementSeriesList);
                   await _unitOfWork.CompleteAsync();
                }
                catch (Exception e)
                {
                    throw;
                }
            }
          await  _parameterService.AssignSpecificationLimits(operation);

            return null;
            }
            catch (Exception)
            {

                return "Zła zawartość pliku";
            }
        }
    }
}



