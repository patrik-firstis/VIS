
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
	public class OrderGateway1
	{
		private readonly SqlConnection _connection;


		public OrderGateway1(SqlConnection connection)
		{
			_connection = connection;
		}

		public void InsertOrder(string customerId, DateTime createdAt)
		{
			var command = new SqlCommand("INSERT INTO Orders (CustomerId, CreatedAt) VALUES (@CustomerId, @CreatedAt)", _connection);
			command.Parameters.AddWithValue("@CustomerId", customerId);
			command.Parameters.AddWithValue("@CreatedAt", createdAt);
			command.ExecuteNonQuery();
		}

		public DataTable GetOrders()
		{
			var adapter = new SqlDataAdapter("SELECT * FROM Orders", _connection);
			var dataTable = new DataTable();
			adapter.Fill(dataTable);
			return dataTable;
		}
	}

}
