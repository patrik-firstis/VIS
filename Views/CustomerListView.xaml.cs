using vistest.ViewModels;

namespace vistest.Views;

public partial class CustomerListView : ContentPage
{
	public CustomerListView()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is CustomerListViewModel vm)
		{
			vm.OnAppearing();
		}
	}
}