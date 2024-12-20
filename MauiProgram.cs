using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using vistest.DataServices;
using vistest.ViewModels;
using vistest.Views;

namespace vistest
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.UseMauiCommunityToolkit()
				.UseMauiCommunityToolkitCore()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});
			
			builder.Services
				.AddSingleton<DbService>()

				.AddSingleton<CarRepository>()
				.AddSingleton<CustomerRepository>()
				.AddSingleton<EmployeeRepository>()
        .AddSingleton<OrderRepository>()
        .AddSingleton<PartRepository>()
				.AddSingleton<PartServisRepository>()
				.AddSingleton<ServisRepository>()

        .AddSingleton<CarService>()
        .AddSingleton<CustomerService>()
        .AddSingleton<EmployeeService>()
        .AddSingleton<OrderService>()
        .AddSingleton<PartService>()
        .AddSingleton<ServisService>()

				.AddSingleton<LoginViewModel>()
				.AddTransient(s => new LoginView()
				{
					BindingContext = s.GetRequiredService<LoginViewModel>()
        })
				.AddSingleton<MainViewModel>()
        .AddTransient(s => new MainView()
				{
					BindingContext = s.GetRequiredService<MainViewModel>()
        })
        .AddSingleton<OrderDetailViewModel>()
        .AddTransient(s => new OrderDetailView()
				{
					BindingContext = s.GetRequiredService<OrderDetailViewModel>()
        })
				.AddSingleton<CustomerDetailViewModel>()
        .AddTransient(s => new CustomerDetailView()
				{
					BindingContext = s.GetRequiredService<CustomerDetailViewModel>()
        })
				.AddSingleton<CarDetailViewModel>()
        .AddTransient(s => new CarDetailView()
				{
					BindingContext = s.GetRequiredService<CarDetailViewModel>()
        })
				.AddSingleton<CustomerListViewModel>()
				.AddTransient(s => new CustomerListView()
				{
					BindingContext = s.GetRequiredService<CustomerListViewModel>()
				})
				.AddSingleton<CarListViewModel>()
				.AddTransient(s => new CarListView()
				{
					BindingContext = s.GetRequiredService<CarListViewModel>()
				})
				.AddSingleton<EmployeeListViewModel>()
				.AddTransient(s => new EmployeeListView()
				{
					BindingContext = s.GetRequiredService<EmployeeListViewModel>()
				})
				.AddSingleton<OrderListViewModel>()
				.AddTransient(s => new OrderListView()
				{
					BindingContext = s.GetRequiredService<OrderListViewModel>()
				})
				.AddSingleton<EmployeeDeatilViewModel>()
				.AddTransient(s => new EmployeeDetailView()
				{
					BindingContext = s.GetRequiredService<EmployeeDeatilViewModel>()
				})
			;



#if DEBUG
      builder.Logging.AddDebug();
#endif

			return builder.Build();
		}
	}
}
