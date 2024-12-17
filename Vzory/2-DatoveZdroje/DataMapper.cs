using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Vzory._2_DatoveZdroje
{
	[Obsolete]
	public class OrderMapper
	{
		private readonly string connectionString;

		public OrderMapper(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public Order3 FindById(int id)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var command = new SqlCommand("SELECT * FROM Orders WHERE id = @id", connection);
				command.Parameters.AddWithValue("@id", id);

				using (var reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						return new Order3
						{
							Id = (int)reader["id"],
							CustomerId = (int)reader["customer_id"],
							Description = reader["description"].ToString(),
							Status = reader["status"].ToString()
						};
					}
				}
			}
			return null; // Nenalezeno
		}

		public void Insert(Order3 order)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var command = new SqlCommand(
						"INSERT INTO Orders (customer_id, description, status) VALUES (@customer_id, @description, @status)",
						connection);
				command.Parameters.AddWithValue("@customer_id", order.CustomerId);
				command.Parameters.AddWithValue("@description", order.Description);
				command.Parameters.AddWithValue("@status", order.Status);
				command.ExecuteNonQuery();
			}
		}
	}
	public class Order3
	{
		public int Id { get; set; }
		public int CustomerId { get; set; }
		public string Description { get; set; }
		public string Status { get; set; }

		// Obchodní logika
		public bool IsCompleted()
		{
			return Status == "Done";
		}
	}

}
