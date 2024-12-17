using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Models
{
	public class Part
	{
		public int Id { get; set; }
		public string Brand { get; set; } = string.Empty;
		public string Model { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;

		public Dictionary<Servis, int> Stock { get; set; } = [];
	}

}
