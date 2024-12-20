using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vistest.Models;

namespace vistest.DataServices
{
  public class EmployeeService : IModelService<Employee>
  {
    private readonly EmployeeRepository _employeeRepository;
    private readonly ServisRepository _servisRepository;
    private readonly Dictionary<int, Employee> _employeeIdentityMap;

    public EmployeeService(EmployeeRepository employeeRepository, ServisRepository servisRepository)
    {
      _employeeRepository = employeeRepository;
      _servisRepository = servisRepository;
      _employeeIdentityMap = [];
    }

    private Employee? Add(Employee employee)
    {
      var id = _employeeRepository.Add(employee);
      var existingEmployee = _employeeRepository.Get(id);
      if (existingEmployee != null)
      {
        _employeeIdentityMap[existingEmployee.Id] = existingEmployee;
      }
      return existingEmployee;
    }

    private Employee Update(Employee employee)
    {
      var existingEmployee = _employeeIdentityMap[employee.Id];
      existingEmployee.Position = employee.Position;
      existingEmployee.EmploymentStartAt = employee.EmploymentStartAt;
      existingEmployee.EmploymentEndAt = employee.EmploymentEndAt;
      existingEmployee.Salary = employee.Salary;
      existingEmployee.Position = employee.Position;

			_employeeRepository.Update(existingEmployee);

      return existingEmployee;
    }

    public void Delete(Employee employee)
    {
      if (_employeeIdentityMap.ContainsKey(employee.Id))
      {
        _employeeRepository.Delete(employee.Id);
        _employeeIdentityMap.Remove(employee.Id);
      }
    }

    public Employee? Get(int id)
    {
      if (_employeeIdentityMap.ContainsKey(id))
      {
        return _employeeIdentityMap[id];
      }

      var employee = _employeeRepository.Get(id);
      if (employee != null)
      {
        employee.Servis = _servisRepository.Get(employee.IdServis)!;
        _employeeIdentityMap[id] = employee;
        return employee;
      }

      return null;
    }

    public List<Employee> GetAll()
    {
      var employees = new List<Employee>(_employeeIdentityMap.Values);

      if (employees.Count == 0)
      {
        employees = _employeeRepository.GetAll();
        foreach (var employee in employees)
        {
          employee.Servis = _servisRepository.Get(employee.IdServis)!;
          _employeeIdentityMap[employee.Id] = employee;
        }
      }

      return employees;
    }

    public Employee? UpdateOrCreate(Employee employee)
    {
      var existingEmployee = Get(employee.Id);
      if (existingEmployee != null)
      {
        return Update(employee);
      }
      else
      {
        return Add(employee);
      }
    }
  }

}
