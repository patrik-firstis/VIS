using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vistest.Models;

namespace vistest.DataServices
{
  public class EmployeeRepository(DbService dbService) : BaseRepository(dbService)
  {
    public int Add(Employee employee)
    {
      string query = @"
                INSERT INTO Employee (id_servis, name, surname, position, employment_start_at, employment_end_at, salary)
                VALUES (@IdServis, @Name, @Surname, @Position, @StartAt, @EndAt, @Salary);";

      var parameters = new Dictionary<string, object?>
            {
                {"@IdServis", employee.IdServis},
                {"@Name", employee.Name},
                {"@Surname", employee.SurName},
                {"@Position", employee.Position},
                {"@StartAt", employee.EmploymentStartAt},
                {"@EndAt", employee.EmploymentEndAt},
                {"@Salary", employee.Salary}
            };

      return ExecuteScalarInsert(query, parameters);
    }

    public void Update(Employee employee)
    {
      string query = @"
                UPDATE Employee
                SET id_servis = @IdServis, name = @Name, surname = @Surname, 
                    position = @Position, employment_start_at = @StartAt, 
                    employment_end_at = @EndAt, salary = @Salary
                WHERE id_employee = @Id;";

      var parameters = new Dictionary<string, object?>
            {
                {"@Id", employee.Id},
                {"@IdServis", employee.IdServis},
                {"@Name", employee.Name},
                {"@Surname", employee.SurName},
                {"@Position", employee.Position},
                {"@StartAt", employee.EmploymentStartAt},
                {"@EndAt", employee.EmploymentEndAt},
                {"@Salary", employee.Salary}
            };

      ExecuteNonQuery(query, parameters);
    }

    public void Delete(int id)
    {
      string query = "DELETE FROM Employee WHERE id_employee = @Id";
      var parameters = new Dictionary<string, object?> { { "@Id", id } };
      ExecuteNonQuery(query, parameters);
    }

    public Employee? Get(int id)
    {
      string query = "SELECT * FROM Employee WHERE id_employee = @Id";
      var parameters = new Dictionary<string, object?> { { "@Id", id } };

      using var reader = ExecuteReader(query, parameters);
      if (reader.Read())
      {
        return new Employee()
        {
          Id = Convert.ToInt32(reader["id_employee"]),
          IdServis = Convert.ToInt32(reader["id_servis"]),
          Name = reader["name"].ToString() ?? string.Empty,
          SurName = reader["surname"].ToString() ?? string.Empty,
          Position = reader["position"].ToString() ?? string.Empty,
          EmploymentStartAt = Convert.ToDateTime(reader["employment_start_at"]),
          EmploymentEndAt = reader["employment_end_at"] == DBNull.Value ? null : Convert.ToDateTime(reader["employment_end_at"]),
          Salary = Convert.ToDouble(reader["salary"])
        };
      }
      return null;
    }

    public List<Employee> GetAll()
    {
      var employees = new List<Employee>();
      string query = "SELECT * FROM Employee";

      using var reader = ExecuteReader(query);
      while (reader.Read())
      {
        employees.Add(new Employee()
        {
          Id = Convert.ToInt32(reader["id_employee"]),
          IdServis = Convert.ToInt32(reader["id_servis"]),
          Name = reader["name"].ToString() ?? string.Empty,
          SurName = reader["surname"].ToString() ?? string.Empty,
          Position = reader["position"].ToString() ?? string.Empty,
          EmploymentStartAt = Convert.ToDateTime(reader["employment_start_at"]),
          EmploymentEndAt = reader["employment_end_at"] == DBNull.Value ? null : Convert.ToDateTime(reader["employment_end_at"]),
          Salary = Convert.ToDouble(reader["salary"])
        });
      }
      return employees;
    }
  }
}
