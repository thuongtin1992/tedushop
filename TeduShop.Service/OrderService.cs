﻿using System;
using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;
using TeduShop.Common.ViewModels;

namespace TeduShop.Service
{
    public interface IOrderService
    {
        Order Create(Order order, List<OrderDetail> orderDetails);
        void UpdateStatus(int orderId);
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

        public Order Create(Order order, List<OrderDetail> orderDetails)
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
                return order;
            }
            catch (Exception ex)
            {
                throw new Exception();
                //do something else
            }
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateStatus(int orderId)
        {
            var order = _orderRepository.GetSingleById(orderId);
            order.Status = true;
            //_orderRepository.Update(order);
        }
    }
}