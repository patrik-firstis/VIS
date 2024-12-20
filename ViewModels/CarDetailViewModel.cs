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
  public partial class CarDetailViewModel : ObservableObject
  {
    private readonly CarService _carService;

    [ObservableProperty]
    private Car car;
    [ObservableProperty]
		private bool _canEdit = false;
    [ObservableProperty]
		private bool _canNotEdit = true;
		public CarDetailViewModel(CarService carService)
    {
      _carService = carService;
    }

    public void OnAppearing()
    {
      Car = AppState.CurrentCar;
			CanEdit = AppState.CurrentEmployee.Position == "Admin" || AppState.CurrentEmployee.Position == "Manager";
			CanNotEdit = !CanEdit;
		}

		[RelayCommand]
    public async Task OnSave()
    {
      if (!CheckCar())
      {
        await Shell.Current.DisplayAlert("Error", "Invalid data", "OK");
        return;
      }

      var result = _carService.UpdateOrCreate(Car);
      if (result == null)
      {
        await Shell.Current.DisplayAlert("Error", "Sql error", "OK");
        return;
      }
      AppState.CurrentCar = result;

      await Shell.Current.GoToAsync("..");
    }

		[RelayCommand]
		public async Task OnDelete()
    {
      _carService.Delete(Car);
      AppState.CurrentCar = new Car();
      await Shell.Current.GoToAsync("..");
    }

    private bool CheckCar()
    {
      if (string.IsNullOrEmpty(Car.Brand))
      {
        return false;
      }
      if (string.IsNullOrEmpty(Car.Model))
      {
        return false;
      }
      if (!string.IsNullOrEmpty(Car.LicencePlate) && Car.LicencePlate.Length < 7)
      {
        return false;
      }
			if (string.IsNullOrEmpty(Car.Vin) || Car.Vin.Length != 17)
			{
				return false;
			}
			if (Car.LastMileage == 0)
      {
        return false;
      }
      return true;
    }
  }
}
