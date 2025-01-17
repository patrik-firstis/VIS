﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vistest.Models;

namespace vistest.DataServices
{
  public class OrderService : IModelService<Order>
  {
    private readonly OrderRepository _orderRepository;
    private readonly CarRepository _carRepository;
    private readonly ServisRepository _servisRepository;
		private readonly CustomerRepository _customerRepository;
    private readonly ScheduleService _scheduleService;
		private readonly Dictionary<int, Order> _orderIdentityMap;

    public OrderService(OrderRepository orderRepository, CarRepository carRepository, ServisRepository servisRepository, CustomerRepository customerRepository, ScheduleService scheduleService)
    {
      _orderRepository = orderRepository;
      _orderIdentityMap = new Dictionary<int, Order>();
      _carRepository = carRepository;
      _servisRepository = servisRepository;
			_customerRepository = customerRepository;
      _scheduleService = scheduleService;
    }

    private Order? Add(Order order)
    {
      // Pridaná funkcionalita na pridanie objednávky do kalendára
      var dates = _scheduleService.AddToSchedule(order.Id);
      order.DateOfStart = dates.Item1;
      order.DateOfFinish = dates.Item2;
      //

      var id = _orderRepository.Add(order);
      var existingOrder = _orderRepository.Get(id);
      if (existingOrder != null)
      {
        _orderIdentityMap[existingOrder.Id] = existingOrder;
      }
      return existingOrder;
    }

    private Order Update(Order order)
    {
      var existingOrder = _orderIdentityMap[order.Id];
      existingOrder.IdCar = order.IdCar;
      existingOrder.IdServis = order.IdServis;
      existingOrder.CreatedAt = order.CreatedAt;
      existingOrder.DateOfStart = order.DateOfStart;
      existingOrder.DateOfFinish = order.DateOfFinish;
      existingOrder.Description = order.Description;
      existingOrder.State = order.State;
      existingOrder.Cost = order.Cost;

      _orderRepository.Update(existingOrder);

      return existingOrder;
    }

    public void Delete(Order order)
    {
      if (_orderIdentityMap.ContainsKey(order.Id))
      {
        _orderRepository.Delete(order.Id);
        _orderIdentityMap.Remove(order.Id);
      }
    }

    public Order? Get(int id)
    {
      if (_orderIdentityMap.ContainsKey(id))
      {
        return _orderIdentityMap[id];
      }

      var order = _orderRepository.Get(id);
      if (order != null)
      {
        order.Car = _carRepository.Get(order.IdCar)!;
        order.Car.Customer = _customerRepository.Get(order.Car.IdCustomer)!;
				order.Servis = _servisRepository.Get(order.IdServis)!;
        _orderIdentityMap[id] = order;
        return order;
      }

      return null;
    }

    public List<Order> GetAll()
    {
      var orders = new List<Order>(_orderIdentityMap.Values);

      if (orders.Count == 0)
      {
        orders = _orderRepository.GetAll();
        foreach (var order in orders)
        {
          order.Car = _carRepository.Get(order.IdCar)!;
          if (order.Car != null)
					  order.Car.Customer = _customerRepository.Get(order.Car.IdCustomer)!;
					order.Servis = _servisRepository.Get(order.IdServis)!;
          _orderIdentityMap[order.Id] = order;
        }
      }

      return orders;
    }

    public Order? UpdateOrCreate(Order order)
    {
      var existingOrder = Get(order.Id);
      if (existingOrder != null)
      {
        return Update(order);
      }
      else
      {
        return Add(order);
      }
    }
  }
}
