using System;
using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;
using TeduShop.Common.ViewModels;

namespace TeduShop.Service
{
    public interface IOrderService
    {
        bool Create(Order order, List<OrderDetail> orderDetails);
        void Save();
    }

    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IOrderDetailRepository _orderDetailRepository;
        private IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IUnitOfWork unitOfWork)
        {
            this._orderRepository = orderRepository;
            this._orderDetailRepository = orderDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public bool Create(Order order, List<OrderDetail> orderDetails)
        {
            try
            {
                _orderRepository.Add(order);

                foreach (var orderDetail in orderDetails)
                {
                    orderDetail.OrderID = order.ID;
                    _orderDetailRepository.Add(orderDetail);
                }

                Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //do something else
            }
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}