using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vistest.Models;

namespace vistest.DataServices
{
  public class CustomerService : IModelService<Customer>
  {
    private readonly CustomerRepository _customerRepository;
    private readonly Dictionary<int, Customer> _customerIdentityMap;

    public CustomerService(CustomerRepository customerRepository)
    {
      _customerRepository = customerRepository;
      _customerIdentityMap =[];
    }

    private Customer? Add(Customer customer)
    {
      var id = _customerRepository.Add(customer);
      var existingCustomer = _customerRepository.Get(id);
      if (existingCustomer != null)
      {
        _customerIdentityMap[existingCustomer.Id] = existingCustomer;
      }
      return existingCustomer;

    }

    private Customer Update(Customer customer)
    {
      var existingCustomer = _customerIdentityMap[customer.Id];
      existingCustomer.Phone = customer.Phone;
      existingCustomer.Email = customer.Email;
      existingCustomer.Name = customer.Name;
      existingCustomer.SurName = customer.SurName;

      _customerRepository.Update(existingCustomer);

      return existingCustomer;
    }

    public void Delete(Customer customer)
    {
      if (_customerIdentityMap.ContainsKey(customer.Id))
      {
        _customerRepository.Delete(customer.Id);
        _customerIdentityMap.Remove(customer.Id);
      }
    }

    public Customer? Get(int id)
    {
      if (_customerIdentityMap.ContainsKey(id))
      {
        return _customerIdentityMap[id];
      }

      var customer = _customerRepository.Get(id);
      if (customer != null)
      {
        _customerIdentityMap[id] = customer;
        return customer;
      }

      return null;
    }

    public List<Customer> GetAll()
    {
      var customers = new List<Customer>(_customerIdentityMap.Values);

      if (customers.Count == 0)
      {
        customers = _customerRepository.GetAll();
        foreach (var customer in customers)
        {
          _customerIdentityMap[customer.Id] = customer;
        }
      }

      return customers;
    }

    public Customer? UpdateOrCreate(Customer customer)
    {
      var existingCustomer = Get(customer.Id);
      if (existingCustomer != null)
      {
        return Update(customer);
      }
      else
      {
        return Add(customer);
      }
    }
  }

}
