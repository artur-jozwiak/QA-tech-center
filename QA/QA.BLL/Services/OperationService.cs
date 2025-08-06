using QA.BLL.Interfaces;
using QA.Domain.Models;
using QA.Domain.Models.Enums;
using QA.Domain.Models.Erp;

namespace QA.BLL.Services
{
    public class OperationService : IOperationService
    {
        private static readonly string[] _sandblastingPrefixes = { "Honowanie", };
        private static readonly string[] _sandblastingOperations = {"Honowanie płytek jedna strona",
                                                                    "Honowanie płytek dwie strony"};

        private static readonly string[] _laboratoryOperationsPrefixes = { "Kontrola SMS/HC" };

        private static readonly string[] _grindingTopAndBottomOperations = { "Szlifowanie płytek dwie strony", "Szlifowanie płytek jedna strona" };

        private static readonly string _pressingOperationSymbol = "WM.2201.NW";

        public bool IsPressingOperation(Operation operation)
        {
            return operation.Symbol == _pressingOperationSymbol;
        }

        public bool IsSandblastingOperation(Operation operation)
        {
            return _sandblastingPrefixes.Any(prefix => operation.Name.StartsWith(prefix));
        }

        public bool IsLaboratoryOperation(Operation operation)
        {
            return _laboratoryOperationsPrefixes.Any(prefix => operation.Name.StartsWith(prefix));
        }

        public bool IsGindingTopAndBottomOperation(Operation operation)
        {
            return _grindingTopAndBottomOperations.Any(name => operation.Name == name);
        }

        public IEnumerable<string> ReturnMissingOperationsIfExist(List<ErpOperation> erpOperations, List<Operation> operations)
        {
            var erpOperationNames = erpOperations.Select(e => e.NazwaOp.Trim()).ToList();
            var operationNames = operations.Select(o => o.Name).ToList();

            bool erpHasNoMatch = erpOperationNames.Any(name => !operationNames.Contains(name));
            bool operationHasNoMatch = operationNames.Any(name => !erpOperationNames.Contains(name));

            if (erpHasNoMatch || operationHasNoMatch)
            {
                return erpOperationNames.Where(name => !operationNames.Contains(name)).ToList();
            }
            return Enumerable.Empty<string>();
        }

        public void CreateOperationDetailsIfNotExist(Operation operation)
        {
            if (operation.OperationDetails == null || operation.OperationDetails.Count == 0)
            {
                OperationType operationType = 0;

                if (IsSandblastingOperation(operation))
                {
                    operationType = OperationType.Sandblasting;
                }//Dodac kolejne typy jesli bedzie potrzeba

                if (operation.Name == _sandblastingOperations[0])
                {
                    operation.OperationDetails = new List<OperationDetails>()
                    {
                        new OperationDetails() {OperationId = operation.Id, Operation = operation, OperationType = operationType},
                    };
                }
                else if (operation.Name == _sandblastingOperations[1])
                {
                    operation.OperationDetails = new List<OperationDetails>()
                    {
                        new OperationDetails() {OperationId = operation.Id, Operation = operation, OperationType = operationType},
                        new OperationDetails() {OperationId = operation.Id, Operation = operation, OperationType = operationType},
                    };
                }
            }
        }

        //public void CreateOperationDetailsIfNotExist(Operation operation)
        //{
        //    if (operation.OperationDetails == null || operation.OperationDetails.Count == 0)
        //    {
        //        OperationType operationType = 0;

        //        if (IsSandblastingOperation(operation))
        //        {
        //            operationType = OperationType.Sandblasting;
        //        }
        //        else if (IsGindingTopAndBottomOperation(operation))
        //        {
        //            operationType = OperationType.GrindinTopAndBottom;
        //        }//Dodac kolejne typy jesli bedzie potrzeba

        //        if (operation.Name == _sandblastingOperations[1])
        //        {
        //            operation.OperationDetails = new List<OperationDetails>()
        //            {
        //                new OperationDetails() {OperationId = operation.Id, Operation = operation, OperationType = operationType},
        //                new OperationDetails() {OperationId = operation.Id, Operation = operation, OperationType = operationType},
        //            };
        //        }
        //        else
        //        {
        //            operation.OperationDetails = new List<OperationDetails>()
        //            {
        //                new OperationDetails() {OperationId = operation.Id, Operation = operation, OperationType = operationType},
        //            };
        //        }

        //        //if (operation.Name == _sandblastingOperations[0])
        //        //{
        //        //    operation.OperationDetails = new List<OperationDetails>()
        //        //    {
        //        //        new OperationDetails() {OperationId = operation.Id, Operation = operation, OperationType = operationType},
        //        //    };
        //        //}
        //        //else if (operation.Name == _sandblastingOperations[1])
        //        //{
        //        //    operation.OperationDetails = new List<OperationDetails>()
        //        //    {
        //        //        new OperationDetails() {OperationId = operation.Id, Operation = operation, OperationType = operationType},
        //        //        new OperationDetails() {OperationId = operation.Id, Operation = operation, OperationType = operationType},
        //        //    };
        //        //}
        //    }
        //}
    }
}
