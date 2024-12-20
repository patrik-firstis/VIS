using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Models
{
	public partial class Order : ObservableObject
	{
		[ObservableProperty]
		private int _id;
		[ObservableProperty]
		private int _idCar;
		[ObservableProperty]
		private int _idServis;
		[ObservableProperty]
		private DateTime _createdAt;
		[ObservableProperty]
		private DateTime? _dateOfStart;
		[ObservableProperty]
		private DateTime? _dateOfFinish;
		[ObservableProperty]
		private string? _description;
		[ObservableProperty]
		private string _state = string.Empty;
		[ObservableProperty]
		private double _cost;
		[ObservableProperty]
		private Car _car = new Car();
		[ObservableProperty]
		private Servis _servis = new Servis();

		public static List<string> StateList { get; } = 
			new List<string> 
			{ "New",
				"Pending",
				"In Progress",
				"Done"
			};

	}

}
