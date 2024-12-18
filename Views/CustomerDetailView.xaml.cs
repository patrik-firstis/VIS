using vistest.ViewModels;

namespace vistest.Views;

public partial class CustomerDetailView : ContentPage
{
	public CustomerDetailView()
	{
		InitializeComponent();
	}

  override protected void OnAppearing()
  {
    base.OnAppearing();
    if (BindingContext is CustomerDetailViewModel vm)
    {
      vm.OnAppearing();
    }
  }
}