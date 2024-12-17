using Microsoft.Maui.ApplicationModel.DataTransfer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Vzory._4_RelacneObjektoveStruktury
{
	[Obsolete]
	public class OrderROS
	{
		public int OrderId { get; set; } // Identity Field = Objekt obsahuje informáciu o svojom identifikátore
		public int CustomerId { get; set; } // Foreign Key Mapping = Objekt nemá informáciu o zákazníkovi, ale obsahuje referenciu na zákazníka
		public DateTime CreatedAt { get; set; }
		public List<Product> Products { get; set; } = new List<Product>();
		public ShippingDetails ShippingDetails { get; set; } // Dependent Mapping Object

		public void GetProducts() // Association mapping = Vzťahu objektov sú uložené v asociačnej tabuľke, obvykle pre N:N vzťahy
		{
			using var _connection = new SqlConnection("Server=.;Database=Northwind;Integrated Security=true");
			var query = @"
						SELECT p.ProductId, p.Name, p.Price
						FROM Orders o
						JOIN OrderProducts op ON o.OrderId = op.OrderId
						JOIN Products p ON op.ProductId = p.ProductId
						WHERE o.OrderId = @OrderId";
			var command = new SqlCommand(query, _connection);
			command.Parameters.AddWithValue("@OrderId", OrderId);

			using (var reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					Products.Add(new Product
					{
						ProductId = (int)reader["ProductId"],
						Name = reader["Name"].ToString(),
						Price = (decimal)reader["Price"]
					});
				}
			}
		}
		public void GetShipingDetails() //Dependent mapping = Objekt obsahuje iný objekt, no v databáze je uložený v rovnakej tabuľke, iba sa namapuje v rámci aplikácie
		{
			using var _connection = new SqlConnection("Server=.;Database=Northwind;Integrated Security=true");
			var query = @"
            SELECT o.OrderId, o.CustomerId, o.CreatedAt, o.Address, o.City, o.Country
            FROM Orders o
            WHERE o.OrderId = @OrderId";

			var command = new SqlCommand(query, _connection);
			command.Parameters.AddWithValue("@OrderId", OrderId);

			using (var reader = command.ExecuteReader())
			{
				if (reader.Read())
				{
					OrderId = (int)reader["OrderId"];
					CustomerId = (int)reader["CustomerId"];
					CreatedAt = (DateTime)reader["CreatedAt"];
					this.ShippingDetails = new ShippingDetails
					{
						Address = reader["Address"].ToString(),
						City = reader["City"].ToString(),
						Country = reader["Country"].ToString()
					};
				}
			}
		}
	}
	public class Product
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
	}
	public class ShippingDetails
	{
		public string Address { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
	}
}
