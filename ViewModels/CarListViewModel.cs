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
using Windows.ApplicationModel.VoiceCommands;
using Windows.Networking.NetworkOperators;

namespace vistest.ViewModels
{
	public partial class CarListViewModel : ObservableObject
	{
		private readonly CarService _carService;
		private bool _sortBrand = false;
		private bool _sortModel = false;
		private bool _sortLicencePlate = false;
		private bool _sortVin = false;
		private bool _sortLastMileage = false;
		private bool _sortCustomer = false;	

		[ObservableProperty]
		private Customer _customer = new();
		[ObservableProperty]
		private List<Car> _cars = [];
		public CarListViewModel(CarService carService)
		{
			_carService = carService;
		}
		public void OnAppearing()
		{
			var cars = _carService.GetAll();
			if (AppState.CurrentCustomer.Id == 0)
			{
				Cars = cars;
			}
			else
			{
				Cars = cars.Where(c => c.IdCustomer == AppState.CurrentCustomer.Id).ToList();
			}
			Customer = AppState.CurrentCustomer;
		}


		[RelayCommand]
		public async void SelectCar(Car car)
		{
			AppState.CurrentCar = car;
			await Shell.Current.Navigation.PopAsync();
		}

		[RelayCommand]
		public async void AddCar()
		{
			if (Customer.Id == 0)
			{
				await Shell.Current.DisplayAlert("Error", "Please select a customer first", "OK");
				return;
			}
			AppState.CurrentCar = new Car() 
			{
				IdCustomer = Customer.Id,
				Customer = Customer
			};
			await Shell.Current.GoToAsync(nameof(CarDetailView));
		}

		[RelayCommand]
		public async void EditCar(Car car)
		{
			AppState.CurrentCar = car;
			await Shell.Current.GoToAsync(nameof(CarDetailView));
		}
		[RelayCommand]
		public async void SelectCustomer()
		{
			AppState.CurrentCustomer = new Customer();
			await Shell.Current.GoToAsync(nameof(CustomerListView));
		}

		[RelayCommand]
		public void SortBrand()
		{
			if (_sortBrand)
				Cars = Cars.OrderBy(c => c.Brand).ToList();
			else
				Cars = Cars.OrderByDescending(c => c.Brand).ToList();

			_sortBrand = !_sortBrand;
		}

		[RelayCommand]
		public void SortModel()
		{
			if (_sortModel)
				Cars = Cars.OrderBy(c => c.Model).ToList();
			else
				Cars = Cars.OrderByDescending(c => c.Model).ToList();

			_sortModel = !_sortModel;
		}

		[RelayCommand]
		public void SortLicencePlate()
		{
			if (_sortLicencePlate)
				Cars = Cars.OrderBy(c => c.LicencePlate).ToList();
			else
				Cars = Cars.OrderByDescending(c => c.LicencePlate).ToList();

			_sortLicencePlate = !_sortLicencePlate;
		}

		[RelayCommand]
		public void SortVin()
		{
			if (_sortVin)
				Cars = Cars.OrderBy(c => c.Vin).ToList();
			else
				Cars = Cars.OrderByDescending(c => c.Vin).ToList();

			_sortVin = !_sortVin;
		}

		[RelayCommand]
		public void SortLastMileage()
		{
			if (_sortLastMileage)
				Cars = Cars.OrderBy(c => c.LastMileage).ToList();
			else
				Cars = Cars.OrderByDescending(c => c.LastMileage).ToList();

			_sortLastMileage = !_sortLastMileage;
		}

		[RelayCommand]
		public void SortCustomer()
		{
			if (_sortCustomer)
				Cars = Cars.OrderBy(c => c.Customer.FullName).ToList();
			else
				Cars = Cars.OrderByDescending(c => c.Customer.FullName).ToList();

			_sortCustomer = !_sortCustomer;
		}

	}
}
