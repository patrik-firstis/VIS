using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Models
{
	public partial class Customer : Person
	{
		[ObservableProperty]
		private string _phone = string.Empty;
		[ObservableProperty]
		private string _email = string.Empty;
	}

}
