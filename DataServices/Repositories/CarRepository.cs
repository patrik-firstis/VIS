using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vistest.Models;

namespace vistest.DataServices
{
  public class CarRepository(DbService dbService) : BaseRepository(dbService)
  {
    public int Add(Car car)
    {
      string query = @"
                INSERT INTO Car (id_customer, brand, model, licence_plate, vin, last_mileage)
                VALUES (@IdCustomer, @Brand, @Model, @LicencePlate, @Vin, @LastMileage);";

      var parameters = new Dictionary<string, object?>
            {
                {"@IdCustomer", car.IdCustomer},
                {"@Brand", car.Brand},
                {"@Model", car.Model},
                {"@LicencePlate", car.LicencePlate},
								{"@Vin", car.Vin},
								{"@LastMileage", car.LastMileage}
            };

      return ExecuteScalarInsert(query, parameters);
    }

    public void Update(Car car)
    {
      string query = @"
                UPDATE Car
                SET id_customer = @IdCustomer, brand = @Brand, model = @Model,
                    licence_plate = @LicencePlate, last_mileage = @LastMileage, vin = @Vin
                WHERE id_car = @Id;";

      var parameters = new Dictionary<string, object?>
            {
                {"@Id", car.Id},
                {"@IdCustomer", car.IdCustomer},
                {"@Brand", car.Brand},
                {"@Model", car.Model},
                {"@LicencePlate", car.LicencePlate},
                {"@LastMileage", car.LastMileage},
								{"@Vin", car.Vin}
						};

      ExecuteNonQuery(query, parameters);
    }

    public void Delete(int id)
    {
      string query = "DELETE FROM Car WHERE id_car = @Id";
      var parameters = new Dictionary<string, object?> { { "@Id", id } };
      ExecuteNonQuery(query, parameters);
    }

    public Car? Get(int id)
    {
      string query = "SELECT * FROM Car WHERE id_car = @Id";
      var parameters = new Dictionary<string, object?> { { "@Id", id } };

      using var reader = ExecuteReader(query, parameters);
      if (reader.Read())
      {
        return new Car()
        {
          Id = Convert.ToInt32(reader["id_car"]),
          IdCustomer = Convert.ToInt32(reader["id_customer"]),
          Brand = reader["brand"].ToString() ?? string.Empty,
          Model = reader["model"].ToString() ?? string.Empty,
          LicencePlate = reader["licence_plate"]?.ToString(),
					Vin = reader["vin"]?.ToString() ?? string.Empty,
					LastMileage = Convert.ToInt32(reader["last_mileage"])
        };
      }
      return null;
    }

    public List<Car> GetAll()
    {
      var cars = new List<Car>();
      string query = "SELECT * FROM Car";

      using var reader = ExecuteReader(query);
      while (reader.Read())
      {
        cars.Add(new Car()
        {
          Id = Convert.ToInt32(reader["id_car"]),
          IdCustomer = Convert.ToInt32(reader["id_customer"]),
          Brand = reader["brand"].ToString() ?? string.Empty,
          Model = reader["model"].ToString() ?? string.Empty,
          LicencePlate = reader["licence_plate"]?.ToString(),
					Vin = reader["vin"]?.ToString() ?? string.Empty,
					LastMileage = Convert.ToInt32(reader["last_mileage"])
        });
      }
      return cars;
    }
  }
}
