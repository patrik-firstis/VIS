using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Models
{
	public enum Position
	{
		Administrator,
		GarageManager,
		Mechanic,
		StorageManager,
	}
	public class Employee : Person
	{
		public int IdServis { get; set; }
		public string Position { get; set; } = string.Empty;
		public DateTime EmploymentStartAt { get; set; }
		public DateTime? EmploymentEndAt { get; set; }
		public double Salary { get; set; }

		public Servis Servis { get; set; }

		public Employee(Servis servis)
		{
			Servis = servis;
		}
	}
}
