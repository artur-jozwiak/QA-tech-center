namespace QA.BLL.Interfaces
{
    public interface IHelperService
    {
        void AssignProductWeight();
        void AssignProductHeight();

        Task AssignQtyToOrders();
        Task ImportHeightStatsToPPDS();
        Task AssignMeasurementToOrder();

        Task AssignPressingPowder();
    }
}
