using vistest.Views;

namespace vistest
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			Routing.RegisterRoute(nameof(MainView), typeof(MainView));
      Routing.RegisterRoute(nameof(OrderDetailView), typeof(OrderDetailView));
			Routing.RegisterRoute(nameof(CarDetailView), typeof(CarDetailView));
			Routing.RegisterRoute(nameof(CustomerDetailView), typeof(CustomerDetailView));
    }
	}
}
