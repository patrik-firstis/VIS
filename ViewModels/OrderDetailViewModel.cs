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
    private string _customerName;
    [ObservableProperty]
    private string _carName;
    [ObservableProperty]
    private bool _isEdit;
    [ObservableProperty]
    private bool _isCustomerSelectable;
    [ObservableProperty]
    private bool _isCarSelectable;

    public OrderDetailViewModel(OrderService orderService)
    {
      _orderService = orderService;
    }

    public void OnAppearing()
    {
      Order = AppState.CurrentOrder;
      IsEdit = Order.Id != 0;
      if (IsEdit)
      {
        CustomerName = Order.Car.Customer.Name + " " +  Order.Car.Customer.SurName;
        CarName = Order.Car.Brand + ' ' + Order.Car.Model + "" + Order.Car.LicencePlate;
      }
      else
      {
        IsCustomerSelectable = true;
      }

      if (AppState.CurrentCustomer.Id != 0)
      {
        CustomerName = AppState.CurrentCustomer.Name + " " + AppState.CurrentCustomer.SurName;
        IsCarSelectable = true;
      }

      if (AppState.CurrentCar.Id != 0)
      {
        CarName = AppState.CurrentCar.Brand + ' ' + AppState.CurrentCar.Model + "" + AppState.CurrentCar.LicencePlate;
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
  }
}
