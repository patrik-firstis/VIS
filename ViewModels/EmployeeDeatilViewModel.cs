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
	public partial class EmployeeDeatilViewModel : ObservableObject
	{
		private readonly EmployeeService _employeeService;

		[ObservableProperty]
		private Employee _employee;
		[ObservableProperty]
		private List<string> _positionList = Employee.PositionList;
		public EmployeeDeatilViewModel(EmployeeService employeeService) 
		{
			_employeeService = employeeService;
		}

		public void OnAppearing()
		{
			Employee = AppState.CurrentEmployee;
		}

		[RelayCommand]
		public async Task Save()
		{
			if (!CheckEmployee())
			{
				await Shell.Current.DisplayAlert("Error", "Please fill all fields", "Ok");
				return;
			}
			var result = _employeeService.UpdateOrCreate(Employee);
			if (result == null)
			{
				await Shell.Current.DisplayAlert("Error", "Sql error", "OK");
				return;
			}
			
			await Shell.Current.Navigation.PopAsync();
		}

		[RelayCommand]
		public async Task Delete()
		{
			_employeeService.Delete(Employee);
			await Shell.Current.Navigation.PopAsync();
		}

		private bool CheckEmployee()
		{
			if (string.IsNullOrEmpty(Employee.Name))
			{
				return false;
			}
			if (string.IsNullOrEmpty(Employee.SurName))
			{
				return false;
			}
			if (string.IsNullOrEmpty(Employee.Position))
			{
				return false;
			}
			if (Employee.Salary == 0)
			{
				return false;
			}
			return true;
		}
	}
}
