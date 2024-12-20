using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vistest.Classes;
using vistest.DataServices;
using vistest.Models;
using vistest.Views;

namespace vistest.ViewModels
{
  public partial class MainViewModel : ObservableObject
  {
    private readonly OrderService _orderService;

    [ObservableProperty]
    private List<Order> _orders = [];
    [ObservableProperty]
    private int _orderTotal = 0;
    [ObservableProperty]
    private int _orderDone = 0;
    [ObservableProperty]
    private int _orderPending = 0;
    [ObservableProperty]
    private int _orderInProgress = 0;

    [ObservableProperty]
    private bool _canCreateOrder = false;
		[ObservableProperty]
		private bool _canSearchEmployees = false;
		[ObservableProperty]
    private bool _canSearchOrders = false;

		public MainViewModel(OrderService orderService)
    {
      _orderService = orderService;
    }

    public void OnAppearing()
    {
      var position = AppState.CurrentEmployee.Position;
      CanSearchEmployees = position == "Admin";
			CanSearchOrders = position == "Admin" || position == "Manager" || position == "Mechanic";
      CanCreateOrder = position == "Admin" || position == "Manager";

			var result = _orderService.GetAll();
      if (AppState.CurrentServis.Id != 0)
      {
         Orders = result.Where(x => x.IdServis == AppState.CurrentServis.Id).ToList();
      }
      else
      {
        Orders = result;
      }
      OrderTotal = Orders.Count;
      OrderDone = Orders.Where(x => x.State == Order.StateList[3]).Count();
      OrderPending = Orders.Where(x => x.State == Order.StateList[1]).Count();
      OrderInProgress = Orders.Where(x => x.State == Order.StateList[2]).Count();

    }

    [RelayCommand]
    public async void OnAddOrder()
    {
      AppState.CurrentOrder = new Order()
      {
        IdServis = AppState.CurrentServis.Id != 0 ? AppState.CurrentServis.Id : AppState.CurrentEmployee.IdServis,
        CreatedAt = DateTime.Now,
        State = Order.StateList[0]
			};

      await Shell.Current.GoToAsync(nameof(OrderDetailView));
    }

    [RelayCommand]
    public async void OnOpenOrders()
    {
			await Shell.Current.GoToAsync(nameof(OrderListView));
		}

    [RelayCommand]
    public async void OnOpenEmployees()
    {
      await Shell.Current.GoToAsync(nameof(EmployeeListView));
		}

  }
}
