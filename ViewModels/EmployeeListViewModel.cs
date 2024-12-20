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
	public partial class EmployeeListViewModel : ObservableObject
	{
		private readonly EmployeeService _employeeService;

		[ObservableProperty]
		private List<Employee> _employees = [];

		private bool _sortName = false;
		private bool _sortSurname = false;
		private bool _sortPosition = false;
		private bool _sortSalary = false;

		public EmployeeListViewModel(EmployeeService employeeService)
		{
			_employeeService = employeeService;
		}

		public void OnAppearing()
		{
			Employees = _employeeService.GetAll();
		}

		[RelayCommand]
		public async void EditEmployee(Employee employee)
		{
			AppState.CurrentEmployee = employee;
			await Shell.Current.GoToAsync(nameof(EmployeeDetailView));
		}

		[RelayCommand]
		public async void AddEmployee()
		{
			AppState.CurrentEmployee = new Employee();
			await Shell.Current.GoToAsync(nameof(EmployeeDetailView));
		}

		[RelayCommand]
		public void SortName()
		{
			if (_sortName)
			{
				Employees = Employees.OrderBy(x => x.Name).ToList();
			}
			else
			{
				Employees = Employees.OrderByDescending(x => x.Name).ToList();
			}
			_sortName = !_sortName;
		}

		[RelayCommand]
		public void SortSurname()
		{
			if (_sortSurname)
			{
				Employees = Employees.OrderBy(x => x.SurName).ToList();
			}
			else
			{
				Employees = Employees.OrderByDescending(x => x.SurName).ToList();
			}
			_sortSurname = !_sortSurname;
		}

		[RelayCommand]
		public void SortPosition()
		{
			if (_sortPosition)
			{
				Employees = Employees.OrderBy(x => x.Position).ToList();
			}
			else
			{
				Employees = Employees.OrderByDescending(x => x.Position).ToList();
			}
			_sortPosition = !_sortPosition;
		}

		[RelayCommand]
		public void SortSalary()
		{
			if (_sortSalary)
			{
				Employees = Employees.OrderBy(x => x.Salary).ToList();
			}
			else
			{
				Employees = Employees.OrderByDescending(x => x.Salary).ToList();
			}
			_sortSalary = !_sortSalary;
		}
	}
}
