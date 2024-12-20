using vistest.ViewModels;

namespace vistest.Views;

public partial class EmployeeDetailView : ContentPage
{
	public EmployeeDetailView()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is EmployeeDeatilViewModel vm)
		{
			vm.OnAppearing();
		}
	}
}