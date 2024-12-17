

namespace vistest.Models
{
	public class Car
	{
		public int Id { get; set; }
		public int IdCustomer { get; set; }
		public string Brand { get; set; } = string.Empty;
		public string Model { get; set; } = string.Empty;
		public string? LicencePlate { get; set; }
		public int LastMileage { get; set; }

		public Customer Customer { get; set; } 
		public List<Order> Orders { get; set; } = [];

		public Car(Customer customer)
		{
			Customer = customer;
		}
	}

}
