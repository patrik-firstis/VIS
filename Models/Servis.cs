using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Models
{
	public partial class Servis : ObservableObject
	{
		[ObservableProperty]
		private int _id;
		[ObservableProperty]
		private string _name = string.Empty;
		[ObservableProperty]
		private string _address = string.Empty;
		[ObservableProperty]
		private DateTime _openedAt;
		[ObservableProperty]
		private DateTime? _closedAt;
	}

}
