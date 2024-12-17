using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Vzory._2_DatoveZdroje
{
	[Obsolete]
	public class Order
	{
		public int Id { get; set; }
		public string CustomerId { get; set; }
		public DateTime CreatedAt { get; set; }

		public void Save(SqlConnection connection)
		{
			var command = new SqlCommand("INSERT INTO Orders (CustomerId, CreatedAt) VALUES (@CustomerId, @CreatedAt)", connection);
			command.Parameters.AddWithValue("@CustomerId", CustomerId);
			command.Parameters.AddWithValue("@CreatedAt", CreatedAt);
			command.ExecuteNonQuery();
		}

		public static Order FindById(int id, SqlConnection connection)
		{
			var command = new SqlCommand("SELECT * FROM Orders WHERE Id = @Id", connection);
			command.Parameters.AddWithValue("@Id", id);
			using var reader = command.ExecuteReader();
			if (reader.Read())
			{
				return new Order
				{
					Id = reader.GetInt32(0),
					CustomerId = reader.GetString(1),
					CreatedAt = reader.GetDateTime(2)
				};
			}
			return null;
		}
	}

}
