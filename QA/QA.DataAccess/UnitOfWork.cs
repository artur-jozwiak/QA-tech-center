using QA.BLL.Interfaces;
using QA.DataAccess.Repositories;
using QA.DataAccess.Repositories.CoatingRepositories;
using QA.DataAccess.Repositories.Keyence;
using QA.DataAccess.Repositories.ToolTesting;

namespace QA.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QAContext _context;

        public UnitOfWork(QAContext context)
        {
            _context = context;
            Parameter = new ParameterRepository(_context);
            Order = new OrderdRepository(_context);
            Operation = new OperationRepository(_context);
            Measurement = new MeasurementsRepository(_context);
            MeasurementSeries = new MeasurementSeriesRepository(_context);
            KeyenceRepository = new KeyenceRepository(_context);
            Product = new ProductRepository(_context);
            Powder = new PowderReposiotry(_context);
            MRB = new MRBRepository(_context);
            User = new UserRepository(_context);
            Image = new ImageRepository(_context);
            Pressing = new PressingRepository(_context);
            Sintering = new SinteringRepository(_context);
            Coating = new CoatingRepository(_context);
            ToolTesting = new ToolTestingRepository(_context);
        }

       public IParameterRepository Parameter { get; private set; }
       public IOrderRepository Order { get; private set; }
       public IOperationRepository Operation { get; private set; }
       public IMeasurementRepository Measurement { get; private set; }
       public IMeasurementSeriesRepository MeasurementSeries { get; private set; }
       public IKeyenceRepository KeyenceRepository { get; private set; }
       public IProductRepository Product { get; private set; }
       public IPowderRepository Powder { get; private set; }
       public IMRBRepository MRB { get; private set; }
       public IUserRepository User { get; private set; }
       public IImageRepository Image { get; private set; }
        public IPressingRepository Pressing { get; private set; }
        public ISinteringRepository Sintering { get; private set; }
        public ICoatingRepository Coating { get; private set; }
        public IToolTestingRepository ToolTesting { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public  int Complete()
        {
            return  _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
