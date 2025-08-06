using QA.BLL.Interfaces;
using QA.Domain.Models;
using QA.Domain.Models.Enums;
using QA.Domain.Models.Erp;

namespace QA.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IErpOperationRepository _erpoperationRepository;
        private readonly IErpOrderRepository _erpOrderRepository;

        public OrderService(IUnitOfWork unitOfWork, IErpOperationRepository erpoperationRepository, IErpOrderRepository erpOrderRepository)
        {
            _unitOfWork = unitOfWork;
            _erpoperationRepository = erpoperationRepository;
            _erpOrderRepository = erpOrderRepository;
        }

        public async Task ChangeStatusIfHasMeasurements(IEnumerable<Order> waitingOrders)
        {
            foreach (var order in waitingOrders)
            {
                var measurementsExist = await _unitOfWork.Measurement.OrderHasMeasurements(order.Id);
                var pressingrecordExist = await _unitOfWork.Pressing.OrderHasPressingRecords(order.Id);

                if (measurementsExist || pressingrecordExist)
                //if (measurementsExist )
                {
                    order.Status = OrderStatus.InProgress;
                }
            }

            await _unitOfWork.CompleteAsync();
        }

        //tylko do migracji
        public async Task<int> MigrateErpOrders()
        {
            int result = 0;
            var erpOrders = await _erpOrderRepository.GetAll();

            foreach (var order in erpOrders)
            {
              result +=  await MigrateErpOrder(order);
            }
            return result;
        }

        public async Task<int> MigrateErpOrder(ErpOrder erpOrder)
        {
            int result = 0;
            var orderKey = erpOrder.KluczZp;

            if (!await _unitOfWork.Order.OrderExist(erpOrder.KluczZp))
            {
                List<ErpOperation> erpOperations = await _erpoperationRepository.GetOperationsByTechId(erpOrder.IdTech);

                Order order = new();
                order.HermesId = erpOrder.Id;
                order.OrderKey = erpOrder.KluczZp;
                order.ShortenedKey = erpOrder.KluczSkrocony;
                order.RowDatetime = new DateTime(1900, 1, 1);
                order.PowderSymbol = erpOrder.SymbolProszku;
                order.PowderBatch = erpOrder.NrPartiiProszku;

                if (!await _unitOfWork.Product.ProductExist(erpOrder.SymbolWyr.Trim()))
                {
                    Product product = new Product
                    {
                        Symbol = erpOrder.SymbolWyr.Trim(),
                        PdmNo = erpOrder.QasymbWnd.Trim(),
                        Description = erpOrder.NazwaArt.Trim(),
                        TechnologyId = erpOrder.IdTech,
                        Orders = new List<Order>(),
                        Operations = new List<Operation>()
                    };
                    product.Orders.Add(order);

                    order.Product = product;

                    foreach (var erpOperation in erpOperations)
                    {
                        Operation operation = new Operation
                        {
                            Name = erpOperation.NazwaOp.Trim(),
                            Symbol = erpOperation.SymbolOp.Trim(),
                            Product = product,
                            TechnologyId = erpOperation.IdTechnolog
                        };
                        product.Operations.Add(operation);
                    }
                }
                else
                {
                    var product = await _unitOfWork.Product.GetProductWitchOrdersBySymbol(erpOrder.SymbolWyr.Trim());

                    foreach (var erpOperation in erpOperations)
                    {
                        if (!product.Operations.Any(o => o.Name == erpOperation.NazwaOp.Trim()))
                        {
                            Operation operation = new Operation
                            {
                                Name = erpOperation.NazwaOp.Trim(),
                                Symbol = erpOperation.SymbolOp.Trim(),
                                Product = product,
                                TechnologyId = erpOperation.IdTechnolog
                            };
                            product.Operations.Add(operation);
                        }
                    }

                    if (product.Orders == null)
                    {
                        product.Orders = new List<Order>();
                    }
                    product.Orders.Add(order);
                    order.Product = product;
                }

                try
                {
                    await _unitOfWork.Order.Add(order);
                    result = await _unitOfWork.CompleteAsync();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return result;
        }
        //tylko do migracji

        //Odpalić tylko do przypisania zleceńń do wpisów z prasowania
        public async Task AssignOrderToPressing()
        {
            var pr = await _unitOfWork.Pressing.GetAll();
            var pressings = pr.Where(p => p.OrderId == null).ToList();
            foreach (var prss in pressings)
            {
                if (prss.PDMNo != null && prss.OrderKey != null)
                {
                    int? orderId = _unitOfWork.Order.GetIdBy(prss.OrderKey.Split('-')[0], prss.PDMNo.Trim());

                    if (orderId != null)
                    {
                        if (prss.OrderId == null)
                        {
                            prss.OrderId = orderId;
                            await _unitOfWork.CompleteAsync();
                        }

                    }
                }
            }
        }
        //Odpalić tylko do przypisania zleceńń do wpisów z prasowania
    }
}
