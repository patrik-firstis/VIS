using vistest.ViewModels;

namespace vistest.Views;

public partial class CarListView : ContentPage
{
	public CarListView()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is CarListViewModel vm)
		{
			vm.OnAppearing();
		}
	}
}