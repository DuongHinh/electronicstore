using ElectronicStore.Data.Core;
using ElectronicStore.Data.Entities;
using ElectronicStore.Data.Repositories;
using ElectronicStore.Service.Projection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ElectronicStore.Data.Entities.Enum.OrderEnum;

namespace ElectronicStore.Service
{
    public interface IOrderService
    {
        bool CreateOrder(Order order, List<OrderDetail> orderDetails);

        Order GetById(int id);

        IEnumerable<Order> GetAll();

        OrderProjection GetDetailOrderByOrderId(int orderId);

        List<OrderProjection> GetAllOrder(string keyword, int? status);

        void Update(Order order);

        void Save();
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

        public void Save()
        {
            this.unitOfWork.Save();
        }

        public void Update(Order order)
        {
            this.orderRepositories.Update(order);
        }

        public List<OrderProjection> GetAllOrder(string keyword, int? status)
        {
            var orders = this.orderRepositories.GetAll();
            var orderDetail = this.orderDetailRepositories.GetAll();

            var orderViewModels = new List<OrderProjection>();

            if (orders != null && orderDetail != null)
            {
                orderViewModels = (from o in orders
                                 join od in orderDetail
                                 on o.Id equals od.OrderId
                                 where status.HasValue ? o.Status == (OrderStatus)status : true
                                 group od by o.Id into gLine
                                 select new OrderProjection
                                 {
                                     OrderId = gLine.Key,
                                     Amount = gLine.Sum(x => x.Price * x.Quantity),
                                     Name = gLine.Select(g => g.Order.Name).FirstOrDefault(),
                                     Address = gLine.Select(g => g.Order.Address).FirstOrDefault(),
                                     Email = gLine.Select(g => g.Order.Email).FirstOrDefault(),
                                     PhoneNumber = gLine.Select(g => g.Order.PhoneNumber).FirstOrDefault(),
                                     OrderDate = gLine.Select(g => g.Order.OrderDate).FirstOrDefault(),
                                     ShipDate = gLine.Select(g => g.Order.ShipDate).FirstOrDefault(),
                                     PaymentStatus = gLine.Select(g => g.Order.PaymentStatus).FirstOrDefault(),
                                     ShipStatus = gLine.Select(g => g.Order.ShipStatus).FirstOrDefault(),
                                     Status = gLine.Select(g => g.Order.Status).FirstOrDefault(),
                                     Products = gLine.Select(g => g.Product).Select(p => new ProductOrderProjection { Name = p.Name, Price = p.Price, Image = p.Image }),
                                     Quantities = gLine.Select(g => g.Quantity)
                                 }).ToList();

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    orderViewModels = orderViewModels.Where(o => o.Name.ToLower().Contains(keyword.ToLower()) || o.Address.ToLower().Contains(keyword.ToLower())).ToList();
                }
            }

            return orderViewModels;
        }

        public OrderProjection GetDetailOrderByOrderId(int orderId)
        {
            var orders = this.orderRepositories.GetAll();
            var orderDetail = this.orderDetailRepositories.GetAll();

            var orderViewModel = new OrderProjection();
            if (orders != null && orderDetail != null)
            {
                orderViewModel = (from o in orders
                                  join od in orderDetail
                                  on o.Id equals od.OrderId
                                  where o.Id == orderId
                                  where od.OrderId == orderId
                                  group od by o.Id into gLine
                                  select new OrderProjection()
                                  {
                                      OrderId = gLine.Key,
                                      Amount = gLine.Sum(x => x.Price * x.Quantity),
                                      Name = gLine.Select(g => g.Order.Name).FirstOrDefault(),
                                      Address = gLine.Select(g => g.Order.Address).FirstOrDefault(),
                                      Email = gLine.Select(g => g.Order.Email).FirstOrDefault(),
                                      PhoneNumber = gLine.Select(g => g.Order.PhoneNumber).FirstOrDefault(),
                                      OrderDate = gLine.Select(g => g.Order.OrderDate).FirstOrDefault(),
                                      ShipDate = gLine.Select(g => g.Order.ShipDate).FirstOrDefault(),
                                      PaymentStatus = gLine.Select(g => g.Order.PaymentStatus).FirstOrDefault(),
                                      ShipStatus = gLine.Select(g => g.Order.ShipStatus).FirstOrDefault(),
                                      Status = gLine.Select(g => g.Order.Status).FirstOrDefault(),
                                      Products = gLine.Select(g => g.Product).Select(p => new ProductOrderProjection{ Id = p.Id, Name = p.Name, Price = p.Price, Image = p.Image }),
                                      Quantities = gLine.Select(g => g.Quantity)
                                  }).FirstOrDefault();
            }

            return orderViewModel;
        }
    }
}
