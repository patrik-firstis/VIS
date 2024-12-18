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
      ;



#if DEBUG
      builder.Logging.AddDebug();
#endif

			return builder.Build();
		}
	}
}
