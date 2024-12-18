using vistest.ViewModels;

namespace vistest.Views;

public partial class CarDetailView : ContentPage
{
	public CarDetailView()
	{
		InitializeComponent();
	}

  override protected void OnAppearing()
  {
    base.OnAppearing();
    if (BindingContext is CarDetailViewModel vm)
    {
      vm.OnAppearing();
    }
  }
}