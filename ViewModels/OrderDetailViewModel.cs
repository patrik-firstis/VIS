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
  public partial class OrderDetailViewModel : ObservableObject
  {
    private readonly OrderService _orderService;

    [ObservableProperty]
    private Order _order;
    [ObservableProperty]
		private bool _isNew = true;
		[ObservableProperty]
    private bool _isNotNew = false;
    [ObservableProperty]
		private bool _canEdit = false;
    [ObservableProperty]
		private bool _canNotEdit = true;
		[ObservableProperty]
    private bool _isCustomerSelectable = false;
    [ObservableProperty]
    private bool _isCarSelectable = false;
    [ObservableProperty]
    private List<string> _stateList = Order.StateList;

    public OrderDetailViewModel(OrderService orderService)
    {
      _orderService = orderService;
    }

    public void OnAppearing()
    {
      Order = AppState.CurrentOrder;
			IsNotNew = Order.Id != 0;
			IsNew = !IsNotNew;
			CanEdit = AppState.CurrentEmployee.Position == "Admin" || AppState.CurrentEmployee.Position == "Manager" || AppState.CurrentEmployee.Position == "Mechanic";
      CanNotEdit = !CanEdit;
			if (!IsNotNew)
      {
        IsCustomerSelectable = true;
      }

      if (AppState.CurrentCustomer.Id != 0 && AppState.CurrentCustomer != Order.Car.Customer && IsNew)
      {
				Order.Car.Customer = AppState.CurrentCustomer;
				IsCarSelectable = true;
      }

      if (AppState.CurrentCar.Id != 0 && AppState.CurrentCar != Order.Car && IsNew)
      {
        Order.Car = AppState.CurrentCar;
        Order.IdCar = AppState.CurrentCar.Id;
			}

    }
    [RelayCommand]
    public async void OnAddCustomer()
    {
      AppState.CurrentCustomer = new Customer();
      await Shell.Current.GoToAsync(nameof(CustomerDetailView));
    }

    [RelayCommand]
    public async void OnAddCar()
    {
      AppState.CurrentCar = new Car()
      {
        IdCustomer = AppState.CurrentCustomer.Id,
        Customer = AppState.CurrentCustomer
      };
      await Shell.Current.GoToAsync(nameof(CarDetailView));
    }
    [RelayCommand]
    public async void OnSelectCustomer()
		{
			await Shell.Current.GoToAsync(nameof(CustomerListView));
		}
    [RelayCommand]
		public async void OnSelectCar()
		{
			await Shell.Current.GoToAsync(nameof(CarListView));
		}

		[RelayCommand]
    public async void OnSave()
    {
      if (!CheckOrder())
			{
				await Shell.Current.DisplayAlert("Error", "Invalid data", "OK");
				return;
			}
      if (Order.Id == 0)
      {
				Order.CreatedAt = DateTime.Now;
				Order.State = Order.StateList[1];
			}
			var result = _orderService.UpdateOrCreate(Order);
			if (result == null)
			{
				await Shell.Current.DisplayAlert("Error", "Sql error", "OK");
				return;
			}

      AppState.CurrentCustomer = new Customer();
			AppState.CurrentCar = new Car();

      await Shell.Current.GoToAsync("..");
		}

		[RelayCommand]
		public async void OnDelete()
		{
			_orderService.Delete(Order);
			AppState.CurrentOrder = new Order();
			await Shell.Current.GoToAsync("..");
		}

		private bool CheckOrder()
    {
      if (Order.Car.Id == 0)
      {
				return false;
			}
			if (Order.Car.Customer.Id == 0)
      {
        return false;
			}
      if (string.IsNullOrEmpty(Order.Description)){
        return false;
			}
			return true;

		}
	}
}
