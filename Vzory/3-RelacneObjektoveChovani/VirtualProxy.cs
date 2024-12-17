using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Vzory._3_RelacneObjektoveChovani
{
	public interface IOrder
	{
		int OrderId { get; }
		int CustomerId { get; }
		DateTime CreatedAt { get; }
	}

	public class Order3 : IOrder
	{
		public int OrderId { get; set; }
		public int CustomerId { get; set; }
		public DateTime CreatedAt { get; set; }
	}

	public class OrderProxy : IOrder
	{
		private readonly int _orderId;
		private readonly SqlConnection _connection;
		private Order3 _realOrder;

		public OrderProxy(int orderId, SqlConnection connection)
		{
			_orderId = orderId;
			_connection = connection;
		}

		// Lazy načítanie reálnej objednávky
		private Order3 RealOrder
		{
			get
			{
				if (_realOrder == null)
				{
					Console.WriteLine("Načítavam objednávku z databázy...");
					var command = new SqlCommand("SELECT * FROM Orders WHERE OrderId = @OrderId", _connection);
					command.Parameters.AddWithValue("@OrderId", _orderId);

					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						_realOrder = new Order3
						{
							OrderId = (int)reader["OrderId"],
							CustomerId = (int)reader["CustomerId"],
							CreatedAt = (DateTime)reader["CreatedAt"]
						};
					}
					reader.Close();
				}

				return _realOrder;
			}
		}

		// Vlastnosti delegujú na reálny objekt, keď je dostupný
		public int OrderId => RealOrder.OrderId;
		public int CustomerId => RealOrder.CustomerId;
		public DateTime CreatedAt => RealOrder.CreatedAt;
	}
}
