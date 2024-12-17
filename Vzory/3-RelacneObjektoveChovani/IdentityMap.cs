using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Vzory._3_RelacneObjektoveChovani
{
	[Obsolete]
	public class OrderIdentityMap
	{
		private readonly Dictionary<int, Order2> _map = new Dictionary<int, Order2>();

		public Order2 GetOrder(SqlConnection connection, int orderId)
		{
			if (_map.ContainsKey(orderId))
			{
				return _map[orderId];
			}

			var command = new SqlCommand("SELECT * FROM Orders WHERE OrderId = @OrderId", connection);
			command.Parameters.AddWithValue("@OrderId", orderId);
			var reader = command.ExecuteReader();

			if (reader.Read())
			{
				var order = new Order2
				{
					OrderId = (int)reader["OrderId"],
					CustomerId = (int)reader["CustomerId"],
					CreatedAt = (DateTime)reader["CreatedAt"]
				};

				_map[order.OrderId] = order;
				return order;
			}

			return null;
		}
	}

	public class Order2
	{
		public int OrderId { get; set; }
		public int CustomerId { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}

