using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Models
{
	public abstract class Person
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string SurName { get; set; } = string.Empty;
	}
}
