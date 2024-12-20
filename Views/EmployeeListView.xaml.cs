using vistest.ViewModels;

namespace vistest.Views;

public partial class EmployeeListView : ContentPage
{
	public EmployeeListView()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is EmployeeListViewModel vm)
		{
			vm.OnAppearing();
		}
	}
}