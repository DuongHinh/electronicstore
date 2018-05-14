using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Repositories
{
    public interface IOrderDetailRepositories : IRepositories<OrderDetail>
    {

    }
    public class OrderDetailRepositories : Repositories<OrderDetail>, IOrderDetailRepositories
    {
        public OrderDetailRepositories(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
