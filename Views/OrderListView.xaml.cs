using vistest.ViewModels;

namespace vistest.Views;

public partial class OrderListView : ContentPage
{
	public OrderListView()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is OrderListViewModel vm)
		{
			vm.OnAppearing();
		}
	}
}