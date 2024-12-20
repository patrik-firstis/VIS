using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using vistest.Classes;
using vistest.DataServices;
using vistest.Models;
using vistest.Views;

namespace vistest.ViewModels
{
	public partial class OrderListViewModel : ObservableObject
	{
		private readonly OrderService _orderService;

		[ObservableProperty]
		private List<Order> _orders = [];

		private bool _sortDate = false;
		private bool _sortCustomer = false;
		private bool _sortCar = false;
		private bool _sortState = false;

		[ObservableProperty]
		private Customer _customer = new();
		[ObservableProperty]
		private Car _car = new();

		public OrderListViewModel(OrderService orderService)
		{
			_orderService = orderService;
		}

		public void OnAppearing()
		{
			Orders = _orderService.GetAll();

			if (AppState.CurrentCustomer.Id != 0)
			{
				Orders = Orders.Where(c => c.Car.IdCustomer == AppState.CurrentCustomer.Id).ToList();
			}
			if (AppState.CurrentCar.Id != 0)
			{
				Orders = Orders.Where(c => c.IdCar == AppState.CurrentCar.Id).ToList();
			}
			Customer = AppState.CurrentCustomer;
			Car = AppState.CurrentCar;
		}

		[RelayCommand]
		public async void EditOrder(Order order)
		{
			AppState.CurrentOrder = order;
			await Shell.Current.GoToAsync(nameof(OrderDetailView));
		}
		[RelayCommand]
		public async void SelectCustomer()
		{
			AppState.CurrentCustomer = new Customer();
			await Shell.Current.GoToAsync(nameof(CustomerListView));
		}

		[RelayCommand]
		public async void SelectCar()
		{
			AppState.CurrentCar = new Car();
			await Shell.Current.GoToAsync(nameof(CarListView));
		}

		[RelayCommand]
		public void SortDate()
		{
			if (_sortDate)
				Orders = Orders.OrderBy(c => c.DateOfStart).ToList();
			else
				Orders = Orders.OrderByDescending(c => c.DateOfStart).ToList();

			_sortDate = !_sortDate;
		}

		[RelayCommand]
		public void SortCustomer()
		{
			if (_sortCustomer)
				Orders = Orders.OrderBy(c => c.Car.Customer.Name).ToList();
			else
				Orders = Orders.OrderByDescending(c => c.Car.Customer.Name).ToList();

			_sortCustomer = !_sortCustomer;
		}

		[RelayCommand]
		public void SortCar()
		{
			if (_sortCar)
				Orders = Orders.OrderBy(c => c.Car.FullName).ToList();
			else
				Orders = Orders.OrderByDescending(c => c.Car.FullName).ToList();

			_sortCar = !_sortCar;
		}

		[RelayCommand]
		public void SortState()
		{
			if (_sortState)
				Orders = Orders.OrderBy(c => c.State).ToList();
			else
				Orders = Orders.OrderByDescending(c => c.State).ToList();

			_sortState = !_sortState;
		}

	}
}
