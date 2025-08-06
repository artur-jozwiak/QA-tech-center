namespace QA.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Order {  get; }
        IParameterRepository Parameter {  get; }
        IOperationRepository Operation {  get; }
        IMeasurementRepository Measurement {  get; }
        IMeasurementSeriesRepository MeasurementSeries { get; }
        IKeyenceRepository KeyenceRepository { get; }
        IProductRepository Product { get; }
        IPowderRepository Powder { get; }
        IMRBRepository MRB { get; }
        IUserRepository User { get; }
        IImageRepository Image { get; }
        IPressingRepository Pressing { get; }
        ISinteringRepository Sintering { get; }
        ICoatingRepository Coating { get; }
        IToolTestingRepository ToolTesting { get; }

        Task<int> CompleteAsync();
        int Complete();
    }
}
