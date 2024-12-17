using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Models
{
	public partial class Part : ObservableObject
	{
		[ObservableProperty]
		private int _id;
		[ObservableProperty]
		private string _brand = string.Empty;
		[ObservableProperty]
		private string _model = string.Empty;
		[ObservableProperty]
		private string _description = string.Empty;

		public Dictionary<Servis, int> Stock { get; set; } = [];
	}

}
