using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using ElectronicStore.Data.Repositories.Projections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;

namespace ElectronicStore.Data.Repositories
{
    public interface IOrderRepositories : IRepositories<Order>
    {
        IEnumerable<ProfitAndRevenueProjection> GetStaticsticProfitAndRevenuePerWeek(string date);
    }

    public class OrderRepositories : Repositories<Order>, IOrderRepositories
    {
        public OrderRepositories(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public IEnumerable<ProfitAndRevenueProjection> GetStaticsticProfitAndRevenuePerWeek(string date)
        {
            IEnumerable<ProfitAndRevenueProjection> query = DbContext.Database.SqlQuery<ProfitAndRevenueProjection>(
                "spGetStaticsticProfitAndRevenuePerWeek @date",
                new SqlParameter("@date", date));
            return query;
        }
    }
}