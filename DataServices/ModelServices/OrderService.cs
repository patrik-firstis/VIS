using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vistest.Models;

namespace vistest.DataServices.ModelServices
{
  public class OrderService : IModelService<Order>
  {
    private readonly OrderRepository _orderRepository;
    private readonly Dictionary<int, Order> _orderIdentityMap;

    public OrderService(OrderRepository orderRepository)
    {
      _orderRepository = orderRepository;
      _orderIdentityMap = new Dictionary<int, Order>();
    }

    private Order? Add(Order order)
    {
      _orderRepository.Add(order);
      var existingOrder = _orderRepository.Get(order.Id);
      if (existingOrder != null)
      {
        _orderIdentityMap[order.Id] = existingOrder;
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
