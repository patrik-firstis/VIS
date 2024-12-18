

using CommunityToolkit.Mvvm.ComponentModel;

namespace vistest.Models
{
	public partial class Car : ObservableObject
	{
		[ObservableProperty]
		private int _id;
		[ObservableProperty]
		private int _idCustomer;
		[ObservableProperty]
		private string _brand = string.Empty;
		[ObservableProperty]
		private string _model = string.Empty;
		[ObservableProperty]
		private string? _licencePlate;
		[ObservableProperty]
		private int _lastMileage;
		[ObservableProperty]
		private Customer _customer = new();
	}

}
