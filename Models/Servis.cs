using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Models
{
	public class Servis
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public DateTime OpenedAt { get; set; }
		public DateTime? ClosedAt { get; set; }

		public ICollection<Employee> Employees { get; set; } = [];
		public ICollection<Order> Orders { get; set; } = [];
		public ICollection<Part> Parts { get; set; } = [];
	}

}
