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

namespace vistest.ViewModels
{
  public partial class CustomerDetailViewModel : ObservableObject
  {
    private readonly CustomerService _customerService;

    [ObservableProperty]
    private Customer _customer;
    public CustomerDetailViewModel(CustomerService customerService)
    {
      _customerService = customerService;
    }

    public void OnAppearing()
    {
      Customer = AppState.CurrentCustomer;
    }

    [RelayCommand]
    public async void OnSave()
    {
      if (!CheckCustomer())
      {
        await Shell.Current.DisplayAlert("Error", "Invalid data", "OK");
        return;
      }
      var result = _customerService.UpdateOrCreate(Customer);
      if (result == null)
      {
        await Shell.Current.DisplayAlert("Error", "Sql error", "OK");
        return;
      }
      AppState.CurrentCustomer = result;
      await Shell.Current.Navigation.PopAsync();
    }

    [RelayCommand]
    public async void OnDelete()
    {
      _customerService.Delete(Customer);
      AppState.CurrentCustomer = new Customer();
      await Shell.Current.Navigation.PopAsync();
    }

    private bool CheckCustomer()
    {
      if (string.IsNullOrEmpty(Customer.Name))
      {
        return false;
      }
      if (string.IsNullOrEmpty(Customer.SurName))
      {
        return false;
      }
      if (string.IsNullOrEmpty(Customer.Phone) || Customer.Phone.Length != 10)
      {
        return false;
      }
      if (string.IsNullOrEmpty(Customer.Email) || !Customer.Email.Contains('@'))
      {
        return false;
      }
      return true;
    }
  }
}
