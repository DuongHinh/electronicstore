using ElectronicStore.Data.Repositories;
using ElectronicStore.Data.Repositories.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Service
{
    public interface IStaticsticService
    {
        IEnumerable<ProfitAndRevenueProjection> GetWeeklyProfitAndRevenue(string date);
    }

    public class StaticsticService : IStaticsticService
    {
        IOrderRepositories orderRepositories;
        public StaticsticService(IOrderRepositories orderRepositories)
        {
            this.orderRepositories = orderRepositories;
        }
        public IEnumerable<ProfitAndRevenueProjection> GetWeeklyProfitAndRevenue(string date)
        {
            return orderRepositories.GetWeeklyProfitAndRevenue(date);
        }
    }
}
