using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Vzory._1_DomenovaLogika
{
	public class Order2
	{
		public int Id { get; set; }
		public string CustomerId { get; set; }
		public List<OrderItem1> Items { get; set; } = new List<OrderItem1>();
		public string Status { get; private set; } = "Pending";
		public DateTime CreatedAt { get; set; }

		public void AddItem(OrderItem1 item)
		{
			Items.Add(item);
		}

		public void Cancel()
		{
			if (Status == "Completed")
				throw new InvalidOperationException("Cannot cancel a completed order.");

			Status = "Cancelled";
		}
	}

	public class OrderRepository2
	{
		public OrderRepository2() { }

	}

}
