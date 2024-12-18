using vistest.ViewModels;

namespace vistest.Views;

public partial class OrderDetailView : ContentPage
{
	public OrderDetailView()
	{
		InitializeComponent();
	}

  protected override void OnAppearing()
  {
    base.OnAppearing();
    if ( BindingContext is OrderDetailViewModel vm)
    {
      vm.OnAppearing();
    }
  }
}