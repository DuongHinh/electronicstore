using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Repositories
{
    public interface IOrderRepositories : IRepositories<Order>
    {

    }
    public class OrderRepositories : Repositories<Order>, IOrderRepositories
    {
        public OrderRepositories(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
