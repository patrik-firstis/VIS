using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Models
{
	public abstract partial class Person : ObservableObject
	{
		[ObservableProperty]
		private int _id;
		[ObservableProperty]
		private string _name = string.Empty;
		[ObservableProperty]
		private string _surName = string.Empty;
	}
}
