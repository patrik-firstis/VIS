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
				new Customer { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Phone = "555-5678", Address = "456 Oak St", City = "Lincoln", Zip = "68508", Country = "USA" },
				new Customer { Id = 3, FirstName = "Michael", LastName = "Johnson", Email = "michael.johnson@example.com", Phone = "555-8765", Address = "789 Pine St", City = "Denver", Zip = "80203", Country = "USA" },
				new Customer { Id = 4, FirstName = "Emily", LastName = "Davis", Email = "emily.davis@example.com", Phone = "555-4321", Address = "101 Maple Ave", City = "Seattle", Zip = "98101", Country = "USA" },
				new Customer { Id = 5, FirstName = "William", LastName = "Brown", Email = "william.brown@example.com", Phone = "555-6789", Address = "202 Birch St", City = "Austin", Zip = "73301", Country = "USA" },
				new Customer { Id = 6, FirstName = "Olivia", LastName = "Wilson", Email = "olivia.wilson@example.com", Phone = "555-1234", Address = "303 Cedar St", City = "Phoenix", Zip = "85001", Country = "USA" },
				new Customer { Id = 7, FirstName = "James", LastName = "Taylor", Email = "james.taylor@example.com", Phone = "555-2345", Address = "404 Elm St", City = "Orlando", Zip = "32801", Country = "USA" },
				new Customer { Id = 8, FirstName = "Sophia", LastName = "Martinez", Email = "sophia.martinez@example.com", Phone = "555-3456", Address = "505 Spruce St", City = "San Francisco", Zip = "94101", Country = "USA" },
				new Customer { Id = 9, FirstName = "David", LastName = "Anderson", Email = "david.anderson@example.com", Phone = "555-4567", Address = "606 Aspen St", City = "New York", Zip = "10001", Country = "USA" },
				new Customer { Id = 10, FirstName = "Ava", LastName = "Thomas", Email = "ava.thomas@example.com", Phone = "555-5678", Address = "707 Chestnut St", City = "Chicago", Zip = "60601", Country = "USA" }
		};

	}
}
