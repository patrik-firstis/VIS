using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vistest.Models;

namespace vistest.DataServices
{
	internal class CustomerServiceStub : ICustomerService
	{
		public bool Add(Customer item)
		{
			return true;
		}

		public bool Delete(Customer item)
		{
			return true;
		}

		public Customer Get(int id)
		{
			return customers[(id%11)-1];
		}

		public List<Customer> GetAll()
		{
			return customers;
		}

		public bool Updat(Customer item)
		{
			return true;
		}

		private List<Customer> customers = new()
		{
				new Customer { Id = 1, Name = "John", SurName = "Doe", Email = "john.doe@example.com", Phone = "555-1234"},
				new Customer { Id = 2, Name = "John", SurName = "Doe", Email = "john.doe@example.com", Phone = "555-1234"},
				new Customer { Id = 3, Name = "John", SurName = "Doe", Email = "john.doe@example.com", Phone = "555-1234"},
				new Customer { Id = 4, Name = "John", SurName = "Doe", Email = "john.doe@example.com", Phone = "555-1234"},
				new Customer { Id = 5, Name = "John", SurName = "Doe", Email = "john.doe@example.com", Phone = "555-1234"},
		};

	}
}
