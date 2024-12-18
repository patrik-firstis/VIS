using vistest.ViewModels;

namespace vistest.Views;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();
	}

  protected override void OnAppearing()
  {
    base.OnAppearing();
    if (BindingContext is LoginViewModel vm)
    {
      vm.OnAppearing();
    }
  }
}