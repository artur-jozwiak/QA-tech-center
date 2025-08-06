using QA.BLL.Interfaces;
using QA.Domain.Models;
using QA.Domain.Models.Erp;
using QA.Domain.Models.OldDb;
//Napisać metode do uzupełniania proszku i partii w tabeli Orders i Pressing- uruchamiać ręcznie
namespace QA.BLL.Services
{
    public class HelperService : IHelperService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISPCService _spcService;
        private readonly IOldDbRepository _oldDbRepository;
        private readonly IErpOrderRepository _erpOrderRepository;

        public HelperService(IUnitOfWork unitOfWork, ISPCService spsService, IOldDbRepository oldDbRepository, IErpOrderRepository erpOrderRepository)
        {
            _unitOfWork = unitOfWork;
            _spcService = spsService;
            _oldDbRepository = oldDbRepository;
            _erpOrderRepository = erpOrderRepository;
        }


        //przed wystawieniem parametrów - odpalać 2 razy dziennie
        //1. SQL Pressing to press02 - wymaga ustawienia daty od której  dane powinny się migrować 
        //2. import danych keyence = Andrzej
        //3. migracja statystyk wysokości do tabeli s1_bk ImportHeightStatsToPPDS - Jeśli rekord istnieje pominie go
        public void AssignProductWeight()
        {
            int produnctWithoutWeight = 0;
            var products =  _unitOfWork.Product.GetWBProductsWithMeasyrements();

            foreach (var product in products)
            {
                var pressings = product.Orders?.SelectMany(o => o.Pressings);
                var weights = new List<decimal>();

                if(pressings.Any(p => p.Weight != null))
                {
                    weights = pressings
                            .Where(p => p.Weight.HasValue)
                            .Select(p => p.Weight.Value)
                            .ToList();
                }

                if (weights.Count() == 0)
                {
                    produnctWithoutWeight++;
                }
                else
                {
                    product.Weight = Math.Round(weights.Average(), 3);
                }
            }

         int count =   _unitOfWork.Complete();
       }

        public void AssignProductHeight()
        {
            int produnctWithoutheight = 0;
            var products = _unitOfWork.Product.GetWBProductsWithMeasyrements();

            foreach (var product in products)
            {
                var pressings = product.Orders?.SelectMany(o => o.Pressings);
                var heights = new List<decimal>();

                if (pressings.Any(p => p.Height1 != null))
                {
                    heights = pressings
                            .Where(p => p.Height1.HasValue)
                            .Select(p => p.Height1.Value)
                            .ToList();
                }

                if (heights.Count() == 0)
                {
                    produnctWithoutheight++;
                }
                else
                {
                    product.SpacerHeight = Math.Round(heights.Average(), 3);
                }
            }
            int count = _unitOfWork.Complete();
        }

        //Do przypisywania ilości do zleceń
        public async Task AssignQtyToOrders()
        {
            var orders =  await _unitOfWork.Order.GetAllAsyn();

            foreach (var order in orders)
            {
                var erpOrder = await _erpOrderRepository.GetBy(order.HermesId);
                if(erpOrder != null)
                {
                    order.Qty = erpOrder.Ilosc;
                }
            }

          var rowsAffectd =  _unitOfWork.Complete();
        }

        public async Task ImportHeightStatsToPPDS()
        {
            //Właczyć OldDbConnection przed uruchomieniem
            int rowsAdded = 0;
            var parameters = await _unitOfWork.Parameter.GetHeightParameters();

            foreach (var parameter in parameters)
            {
                var measurements = parameter.Measurements;
                var gruppedMeasurements = measurements.GroupBy(m => m.OrderKey);

                foreach (var group in gruppedMeasurements)
                {
                    var measurementsValues = group.Select(g => g.Value).ToList();
                    decimal avg = _spcService.CalculateAvg(measurementsValues);
                    decimal standardDefiation = _spcService.CalculateStandardDeviation(measurementsValues, avg);
                    decimal maxValue = _spcService.CalculateMax(measurementsValues);
                    decimal minValue = _spcService.CalculateMin(measurementsValues);

                    S1Bk newRecord = new S1Bk()
                    {
                        Parameter = parameter.Name,
                        Unit = parameter.Unit,
                        Usl = (double)parameter.USL,
                        Lsl = (double)parameter.LSL,
                        Qty = group.Count(),
                        MaxValue = (double)maxValue,
                        AvgValue = (double)avg,
                        MinValue = (double)minValue,
                        Cp = (double)_spcService.CalculateCp(parameter.USL, parameter.LSL, standardDefiation),
                        Cpk = (double)_spcService.CalculateCpk(parameter.USL, parameter.LSL, avg, standardDefiation),
                        Ro = (double)standardDefiation,
                        Delta = (double)_spcService.CalculateDelta(maxValue, minValue),
                        OrderNo = group.Key,
                        Nominal = (double)parameter.NominalValue,
                    };

                    if (!_oldDbRepository.RecordExist(parameter.Name, group.Key))
                    {
                        await _oldDbRepository.Add(newRecord);
                        rowsAdded++;
                    }
                }
            }
            //rowsAffected i rowsAdded to to samo 
            int rowsAffected = await _oldDbRepository.Complete();
        }

        //Ładowanie danych historycznych z laboratorium
        //SMHCAnalysis.LoadProductData(_products);// ostatnie uruchomienie 05.02.25
        //tylko do uzupełniania braków Powder w tabeli Pressing


        //Metoda do aktualizacji proszku i partij w tabelach Orders i Pressing
        public async Task AssignPressingPowder()
        {
            int updatedPowder = 0;
            int updatedOrder = 0;
            int ordrsnotRegisteredInQA = 0;
            int orderWihoutPowder = 0;

            var pressings = await _unitOfWork.Pressing.GetAll();

            var pressingsWithoutPowder = pressings.Where(p => String.IsNullOrEmpty(p.Powder)).ToList();

            foreach (var pressing in pressingsWithoutPowder)
            {
                Order? order = await _unitOfWork.Order.GetByShortenedKey(pressing.OrderKey.Split("-")[0]);
                //pobrać zlecenie z widoku

                if (order != null)
                {
                    ErpOrder erpOrder = await _erpOrderRepository.GetBy(order.HermesId);

                    //if (order.PowderBatch != null && order.PowderSymbol != null)
                    if (erpOrder.SymbolProszku != null && erpOrder.NrPartiiProszku != null)
                    {
                        //pressing.Powder = string.Concat(order.PowderSymbol.Replace("M-H", "").Trim(), "/", order.PowderBatch.Trim());
                        if(order.PowderSymbol == null && order.PowderBatch == null)
                        {
                            order.PowderSymbol = erpOrder.SymbolProszku;
                            order.PowderBatch = erpOrder.NrPartiiProszku;
                            updatedOrder++;
                            Console.WriteLine($"Aktualizacja zlecenia: {erpOrder.KluczSkrocony}: {order.PowderSymbol}, {order.PowderBatch}");

                        }

                        pressing.Powder = string.Concat(erpOrder.SymbolProszku.Replace("M-H", "").Trim(), "/", erpOrder.NrPartiiProszku.Trim());
                        updatedPowder++;
                        Console.WriteLine($"Aktualizacja pressing: {erpOrder.KluczSkrocony}: {pressing.Powder}");
                        _unitOfWork.Pressing.Update(pressing);
                    }
                    else
                    {
                        orderWihoutPowder++;
                    }
                }
                else
                {
                    ordrsnotRegisteredInQA++;
                }
            }
            await Console.Out.WriteLineAsync($"Dodanie proszku do pressing:{updatedPowder}");
            await Console.Out.WriteLineAsync($"Dodanie proszku do order:{updatedOrder}");
            await Console.Out.WriteLineAsync($"Nie zarejestrowanych zleceń:{ordrsnotRegisteredInQA}");
            await Console.Out.WriteLineAsync($"Zleceń erp bez proszku:{orderWihoutPowder}");

            _unitOfWork.Complete();
        }

        //Uzywane do przypisywania Orderid dla pomiarów z laboratorium - użyć w metodzie pobierającej zlecenia do keyenceMeasurements, Measurements i Pressing bez przypisanych ORDERID
        public async Task AssignMeasurementToOrder()
        {
            int rowsModified = 0;
            var orders = await _unitOfWork.Order.GetAllAsyn();
            var measurements = _unitOfWork.Measurement.GetMeasurementsWithoutOrderAssigned();

            foreach (var order in orders)
            {
                if (measurements.Any(m => m.OrderKey == order.ShortenedKey))
                {
                    var measurementsToAssign = measurements.Where(m => m.OrderKey == order.ShortenedKey);

                    foreach (var measurement in measurementsToAssign)
                    {
                        measurement.OrderId = order.Id;
                        rowsModified++;
                    }
                }
            }
            int rowsAffected = _unitOfWork.Complete();
        }

        //nie uzyte bo po przypisaniu serii trzeba by było poprawiać LaboratoryTable bo sie kolumny nie zgadzają
        public async Task AssignMeasurementSeriesToOrder()
        {
            int rowsModified = 0;
            var orders = await _unitOfWork.Order.GetAllAsyn();
            var measurementsSeries = _unitOfWork.MeasurementSeries.GetMeasurementSeriesesWithoutOrderAssigned();

            foreach (var order in orders)
            {
                if (measurementsSeries.Any(ms => ms.Measurements.Any(m => m.OrderKey == order.ShortenedKey)))
                {
                    var seriesToAssign = measurementsSeries.Where(m => m.Measurements.Any(m => m.OrderKey == order.ShortenedKey));

                    foreach (var series in seriesToAssign)
                    {
                        series.OrderId = order.Id;
                        rowsModified++;
                    }
                }
            }
            int rowsAffected = _unitOfWork.Complete();
        }


        //do przypidywania prozku do zlecen - wyciągnięte z Orders i nie uruchamiane
        //private async Task AssignPowder()
        //{
        //    foreach (var order in _orders)
        //    {
        //        var erpOrder = await ERPContext.ErpOrders.FirstOrDefaultAsync(o => o.KluczZp == order.OrderKey);

        //        if (erpOrder != null)
        //        {
        //            order.PowderSymbol = erpOrder.SymbolProszku?.Trim();
        //            order.PowderBatch = erpOrder.NrPartiiProszku?.Trim();

        //            await UnitOfWork.CompleteAsync();
        //        }
        //        else
        //        {
        //            Console.WriteLine(order.OrderKey);
        //        }
        //    }
        //}
    }
}
