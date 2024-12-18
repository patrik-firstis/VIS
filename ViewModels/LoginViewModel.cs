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
  public partial class LoginViewModel : ObservableObject
  {
    private readonly EmployeeService EmployeeService;
    private readonly ServisService ServisService;

    [ObservableProperty]
    public string _username = string.Empty;
    [ObservableProperty]
    public string _password = string.Empty;

    public LoginViewModel(EmployeeService employeeService, ServisService servisService)
    {
      EmployeeService = employeeService;
      ServisService = servisService;
    }

    public void OnAppearing()
    {
      ServisService.UpdateOrCreate(new Servis { Id = 1, Name = "Servis 1", Address = "Address 1" });
      EmployeeService.UpdateOrCreate(new Employee { Id = 1, IdServis = 1, Position = "Admin", Salary = 1000 });
      AppState.CurrentServis = new Servis();
      AppState.CurrentEmployee = new Employee();
    }

    [RelayCommand]
    async void OnLogin()
    {
      if (Username == "admin" && Password == "admin")
      {
        var employee = EmployeeService.Get(1);
        if (employee == null)
        {
          await Shell.Current.DisplayAlert("Error", "Invalid username or password", "OK");
          return;
        }
        AppState.CurrentEmployee = employee;
        AppState.CurrentServis = new Servis();
      }
      else if (Username == "manager" && Password == "manager")
      {
        var employee = EmployeeService.Get(2);
        if (employee == null)
        {
          await Shell.Current.DisplayAlert("Error", "Invalid username or password", "OK");
          return;
        }
        AppState.CurrentEmployee = employee;
        AppState.CurrentServis = employee.Servis;
      }
      else if (Username == "mechanic" && Password == "mechanic")
      {
        var employee = EmployeeService.Get(3);
        if (employee == null)
        {
          await Shell.Current.DisplayAlert("Error", "Invalid username or password", "OK");
          return;
        }
        AppState.CurrentEmployee = employee;
        AppState.CurrentServis = employee.Servis;
      }
      else
      {
        await Shell.Current.DisplayAlert("Error", "Invalid username or password", "OK");
        return;
      }

      await Shell.Current.GoToAsync($"{nameof(MainView)}");
    }
  }
}
