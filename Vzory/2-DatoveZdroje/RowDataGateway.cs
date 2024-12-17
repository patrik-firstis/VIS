using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Vzory._2_DatoveZdroje
{
	[Obsolete]
	public class OrderGateway
	{
		private readonly SqlConnection _connection;
		private int _orderId; 

		// Constructor for creating a new order (insert)
		public OrderGateway(SqlConnection connection)
		{
			_connection = connection;
		}

		// Constructor for fetching an existing order (get)
		public OrderGateway(SqlConnection connection, int orderId)
		{
			_connection = connection;
			_orderId = orderId;
		}

		// Insert the current order into the database
		public void InsertOrder(string customerId, DateTime createdAt)
		{
			var command = new SqlCommand("INSERT INTO Orders (CustomerId, CreatedAt) VALUES (@CustomerId, @CreatedAt)", _connection);
			command.Parameters.AddWithValue("@CustomerId", customerId);
			command.Parameters.AddWithValue("@CreatedAt", createdAt);

			try
			{
				command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error inserting order: {ex.Message}");
			}
		}

		// Get the order from the database (based on orderId)
		public DataRow GetOrder()
		{
			if (_orderId == 0)
				throw new InvalidOperationException("OrderId is not specified.");

			var adapter = new SqlDataAdapter("SELECT * FROM Orders WHERE OrderId = @OrderId", _connection);
			adapter.SelectCommand.Parameters.AddWithValue("@OrderId", _orderId);
			var dataTable = new DataTable();

			try
			{
				adapter.Fill(dataTable);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error fetching order: {ex.Message}");
			}

			return dataTable.Rows.Count > 0 ? dataTable.Rows[0] : null;
		}
	}

}
