using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Models
{
	public class Customer : Person
	{
		public string Phone { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		
		public List<Car> Cars { get; set; } = [];
	}

}
