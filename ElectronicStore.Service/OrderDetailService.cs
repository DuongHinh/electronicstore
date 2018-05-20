using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using ElectronicStore.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Service
{
    public interface IOrderDetailService
    {
        IEnumerable<OrderDetail> GetAll();
    }
    public class OrderDetailService : IOrderDetailService
    {
        private IOrderDetailRepositories orderDetailRepositories;
        private IUnitOfWork unitOfWork;
        public OrderDetailService(IOrderRepositories orderRepositories, IOrderDetailRepositories orderDetailRepositories, IUnitOfWork unitOfWork)
        {
            this.orderDetailRepositories = orderDetailRepositories;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return this.orderDetailRepositories.GetAll();
        }
    }
}
