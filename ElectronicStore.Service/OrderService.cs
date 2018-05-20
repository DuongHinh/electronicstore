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
    public interface IOrderService
    {
        bool CreateOrder(Order order, List<OrderDetail> orderDetails);

        Order GetById(int id);

        IEnumerable<Order> GetAll();
    }

    public class OrderService : IOrderService
    {
        private IOrderRepositories orderRepositories;
        private IOrderDetailRepositories orderDetailRepositories;
        private IUnitOfWork unitOfWork;
        public OrderService(IOrderRepositories orderRepositories, IOrderDetailRepositories orderDetailRepositories, IUnitOfWork unitOfWork)
        {
            this.orderRepositories = orderRepositories;
            this.orderDetailRepositories = orderDetailRepositories;
            this.unitOfWork = unitOfWork;
        }

        public bool CreateOrder(Order order, List<OrderDetail> orderDetails)
        {
            try
            {
                this.orderRepositories.Add(order);
                this.unitOfWork.Save();

                foreach (var orderDetail in orderDetails)
                {
                    orderDetail.OrderId = order.Id;
                    this.orderDetailRepositories.Add(orderDetail);
                }
                this.unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Order GetById(int id)
        {
            return this.orderRepositories.GetSingleById(id);
        }

        public IEnumerable<Order> GetAll()
        {
            return this.orderRepositories.GetAll();
        }
    }
}
