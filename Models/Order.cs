using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Models
{
	public class Order
	{
		public int Id { get; set; }
		public int IdCar { get; set; }
		public int IdServis { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? DateOfStart { get; set; }
		public DateTime? DateOfFinish { get; set; }
		public string? Description { get; set; }
		public string State { get; set; } = string.Empty;
		public double Cost { get; set; }

		public Car Car { get; set; }
		public Servis Servis { get; set; }

		public Order(Car car, Servis servis)
		{
			Car = car;
			Servis = servis;
		}
	}

}
