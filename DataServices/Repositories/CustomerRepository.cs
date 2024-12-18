
using System.Data.SQLite;
using vistest.Models;

namespace vistest.DataServices
{
  public class CustomerRepository(DbService dbService) : BaseRepository(dbService)
  {
    public int Add(Customer customer)
    {
        string query = @"
                INSERT INTO Customer (name, surname, email, phone)
                VALUES (@Name, @Surname, @Email, @Phone);";

        var parameters = new Dictionary<string, object?>
            {
                {"@Name", customer.Name},
                {"@Surname", customer.SurName},
                {"@Phone", customer.Phone},
                {"@Email", customer.Email}
            };

       return ExecuteScalarInsert(query, parameters);
    }

    public void Update(Customer customer)
    {
      string query = @"
                UPDATE Customer
                SET name = @Name, surname = @Surname, phone = @Phone, email = @Email
                WHERE id_customer = @Id;";

      var parameters = new Dictionary<string, object?>
            {
                {"@Id", customer.Id},
                {"@Name", customer.Name},
                {"@Surname", customer.SurName},
                {"@Phone", customer.Phone},
                {"@Email", customer.Email}
            };

      ExecuteNonQuery(query, parameters);
    }

    public void Delete(int id)
    {
      string query = "DELETE FROM Customer WHERE id_customer = @Id";
      var parameters = new Dictionary<string, object?> { { "@Id", id } };
      ExecuteNonQuery(query, parameters);
    }

    public Customer? Get(int id)
    {
      string query = "SELECT * FROM Customer WHERE id_customer = @Id";
      var parameters = new Dictionary<string, object?> { { "@Id", id } };

      using var reader = ExecuteReader(query, parameters);
      if (reader.Read())
      {
        return new Customer
        {
          Id = Convert.ToInt32(reader["id_customer"]),
          Name = reader["name"].ToString() ?? string.Empty,
          SurName = reader["surname"].ToString() ?? string.Empty,
          Phone = reader["phone"].ToString() ?? string.Empty,
          Email = reader["email"].ToString() ?? string.Empty
        };
      }
      return null;
    }

    public List<Customer> GetAll()
    {
      var customers = new List<Customer>();
      string query = "SELECT * FROM Customer";

      using var reader = ExecuteReader(query);
      while (reader.Read())
      {
        customers.Add(new Customer
        {
          Id = Convert.ToInt32(reader["id_customer"]),
          Name = reader["name"].ToString() ?? string.Empty,
          SurName = reader["surname"].ToString() ?? string.Empty,
          Phone = reader["phone"].ToString() ?? string.Empty,
          Email = reader["email"].ToString() ?? string.Empty
        });
      }

      return customers;
    }
  }
}
