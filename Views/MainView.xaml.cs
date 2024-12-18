using vistest.ViewModels;

namespace vistest.Views;

public partial class MainView : ContentPage
{
	public MainView()
	{
		InitializeComponent();
	}

  override protected void OnAppearing()
  {
    base.OnAppearing();
    if (BindingContext is MainViewModel vm)
    {
      vm.OnAppearing();
    }
  }
}