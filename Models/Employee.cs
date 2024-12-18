using CommunityToolkit.Mvvm.ComponentModel;
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
	public partial class Employee : Person
	{
		[ObservableProperty]
		private int _idServis;
		[ObservableProperty]
		private string _position = string.Empty;
		[ObservableProperty]
		private DateTime _employmentStartAt;
		[ObservableProperty]
		private DateTime? _employmentEndAt;
		[ObservableProperty]
		private double _salary;
		[ObservableProperty]
		private Servis _servis;
	}
}
