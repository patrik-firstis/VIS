using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using vistest.Classes;
using vistest.DataServices;
using vistest.Models;
using vistest.Views;

namespace vistest.ViewModels
{
	public partial class CustomerListViewModel : ObservableObject
	{
		private readonly CustomerService _customerService;

		private bool _sortName = false;
		private bool _sortSurname = false;
		private bool _sortPhone = false;
		private bool _sortEmail = false;

		[ObservableProperty]
		private List<Customer> _customers = [];
		public CustomerListViewModel(CustomerService customerService)
		{
			_customerService = customerService;
		}

		public void OnAppearing()
		{
			AppState.CurrentCustomer = new Customer();
			Customers = _customerService.GetAll();
		}

		[RelayCommand]
		public async void SelectCustomer(Customer customer)
		{
			AppState.CurrentCustomer = customer;
			await Shell.Current.Navigation.PopAsync();
		}

		[RelayCommand]
		public async void AddCustomer()
		{
			AppState.CurrentCustomer = new Customer();
			await Shell.Current.GoToAsync(nameof(CustomerDetailView));
		}

		[RelayCommand]
		public async void EditCustomer(Customer customer)
		{
			AppState.CurrentCustomer = customer;
			await Shell.Current.GoToAsync(nameof(CustomerDetailView));
		}

		[RelayCommand]
		public void SortName()
		{ 
			if (_sortName)
				Customers = Customers.OrderBy(c => c.Name).ToList();
			else
				Customers = Customers.OrderByDescending(c => c.Name).ToList();

			_sortName = !_sortName;
		}

		[RelayCommand]
		public void SortSurname()
		{
			if (_sortSurname)
				Customers = Customers.OrderBy(c => c.SurName).ToList();
			else
				Customers = Customers.OrderByDescending(c => c.SurName).ToList();

			_sortSurname = !_sortSurname;
		}

		[RelayCommand]
		public void SortPhone()
		{
			if (_sortPhone)
				Customers = Customers.OrderBy(c => c.Phone).ToList();
			else
				Customers = Customers.OrderByDescending(c => c.Phone).ToList();

			_sortPhone = !_sortPhone;
		}

		[RelayCommand]
		public void SortEmail()
		{
			if (_sortEmail)
				Customers = Customers.OrderBy(c => c.Email).ToList();
			else
				Customers = Customers.OrderByDescending(c => c.Email).ToList();

			_sortEmail = !_sortEmail;
		}
	}
}
