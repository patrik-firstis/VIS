using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Vzory._1_DomenovaLogika
{
	public class OrderRepository1
	{
		public void Save(Order1 order)
		{
			// Save order to database
		}
		public Order1 GetById(int orderId)
		{
			// Get order from database
			return new Order1();
		}
	}
	public class OrderItem1
	{
		public string ProductId { get; set; }
		public int Quantity { get; set; }
	}

	public class Order1
	{
		public string CustomerId { get; set; }
		public List<OrderItem1> Items { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Status { get; set; }
	}
  public class OrderService1
	{
		private readonly OrderRepository1 _orderRepository;

		public OrderService1(OrderRepository1 orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public void CreateOrder(string customerId, List<OrderItem1> items)
		{
			var order = new Order1
			{
				CustomerId = customerId,
				Items = items,
				CreatedAt = DateTime.UtcNow
			};
			_orderRepository.Save(order);
		}

		public void AddItemToOrder(int orderId, OrderItem1 item)
		{
			var order = _orderRepository.GetById(orderId);
			if (order == null) throw new Exception("Order not found.");

			order.Items.Add(item);
			_orderRepository.Save(order);
		}

		public void CancelOrder(int orderId)
		{
			var order = _orderRepository.GetById(orderId);
			if (order == null) throw new Exception("Order not found.");

			order.Status = "Cancelled";
			_orderRepository.Save(order);
		}
	}

}
