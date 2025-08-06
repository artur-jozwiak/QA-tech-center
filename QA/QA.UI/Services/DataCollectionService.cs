using QA.BLL.Interfaces;

namespace QA.UI.Services
{
    public class DataCollectionService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private const int _delayInMinutes = 60;
       // private const int _delayInMinutes = 1;//test

        public DataCollectionService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(TimeSpan.FromMinutes(10));
            while (!stoppingToken.IsCancellationRequested)
            {
                await GetData();
                await Task.Delay(TimeSpan.FromMinutes(_delayInMinutes), stoppingToken);
            }
        }

        private async Task GetData()
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var keyenceService = scope.ServiceProvider.GetRequiredService<IKeyenceReader>();
                    //var pvdStatsService = scope.ServiceProvider.GetRequiredService<IPVDStatsRepository>();
                    //tu dodawac kolejne serwisy i metody do pobierania danych

                    await keyenceService.GetKeyenceData();
                    //czy pobierać cyklicznie pliki pvd - pobierać automatycznie i umizliwić pobranie ręczne
                    //pvdStatsService.ReadPVDStatsLastReport();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //private async Task GetData()
        //{
        //    try
        //    {
        //        using (var scope = _serviceProvider.CreateScope())
        //        {
        //            var keyenceService = scope.ServiceProvider.GetRequiredService<IKeyenceReader>();
        //            await keyenceService.GetKeyenceData();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}


