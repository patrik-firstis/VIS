using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vistest.Models;

namespace vistest.DataServices
{
  public class OrderRepository(DbService dbService) : BaseRepository(dbService)
  {

    public int Add(Order order)
    {
      string query = @"
                INSERT INTO 'Order' 
                (id_car, id_servis, created_at, date_of_start, date_of_finish, description, state, cost)
                VALUES 
                (@IdCar, @IdServis, @CreatedAt, @DateOfStart, @DateOfFinish, @Description, @State, @Cost);";

      var parameters = new Dictionary<string, object?>
            {
                {"@IdCar", order.IdCar},
                {"@IdServis", order.IdServis},
                {"@CreatedAt", order.CreatedAt},
                {"@DateOfStart", order.DateOfStart},
                {"@DateOfFinish", order.DateOfFinish},
                {"@Description", order.Description},
                {"@State", order.State},
                {"@Cost", order.Cost}
            };

      return ExecuteScalarInsert(query, parameters);
    }

    public void Update(Order order)
    {
      string query = @"
                UPDATE 'Order'
                SET id_car = @IdCar, 
                    id_servis = @IdServis, 
                    created_at = @CreatedAt, 
                    date_of_start = @DateOfStart, 
                    date_of_finish = @DateOfFinish, 
                    description = @Description, 
                    state = @State, 
                    cost = @Cost
                WHERE id_order = @Id;";

      var parameters = new Dictionary<string, object?>
            {
                {"@Id", order.Id},
                {"@IdCar", order.IdCar},
                {"@IdServis", order.IdServis},
                {"@CreatedAt", order.CreatedAt},
                {"@DateOfStart", order.DateOfStart},
                {"@DateOfFinish", order.DateOfFinish},
                {"@Description", order.Description},
                {"@State", order.State},
                {"@Cost", order.Cost}
            };

      ExecuteNonQuery(query, parameters);
    }

    public void Delete(int id)
    {
      string query = "DELETE FROM 'Order' WHERE id_order = @Id;";
      var parameters = new Dictionary<string, object?> { { "@Id", id } };
      ExecuteNonQuery(query, parameters);
    }

    public Order? Get(int id)
    {
      string query = "SELECT * FROM 'Order' WHERE id_order = @Id;";
      var parameters = new Dictionary<string, object?> { { "@Id", id } };

      using var reader = ExecuteReader(query, parameters);
      if (reader.Read())
      {
        return new Order
        {
          Id = Convert.ToInt32(reader["id_order"]),
          IdCar = Convert.ToInt32(reader["id_car"]),
          IdServis = Convert.ToInt32(reader["id_servis"]),
          CreatedAt = Convert.ToDateTime(reader["created_at"]),
          DateOfStart = reader["date_of_start"] == DBNull.Value ? null : Convert.ToDateTime(reader["date_of_start"]),
          DateOfFinish = reader["date_of_finish"] == DBNull.Value ? null : Convert.ToDateTime(reader["date_of_finish"]),
          Description = reader["description"].ToString(),
          State = reader["state"].ToString() ?? string.Empty,
          Cost = Convert.ToDouble(reader["cost"])
        };
      }
      return null;
    }

    public List<Order> GetAll()
    {
      var orders = new List<Order>();
      string query = "SELECT * FROM 'Order';";

      using var reader = ExecuteReader(query);
      while (reader.Read())
      {
        orders.Add(new Order
        {
          Id = Convert.ToInt32(reader["id_order"]),
          IdCar = Convert.ToInt32(reader["id_car"]),
          IdServis = Convert.ToInt32(reader["id_servis"]),
          CreatedAt = Convert.ToDateTime(reader["created_at"]),
          DateOfStart = reader["date_of_start"] == DBNull.Value ? null : Convert.ToDateTime(reader["date_of_start"]),
          DateOfFinish = reader["date_of_finish"] == DBNull.Value ? null : Convert.ToDateTime(reader["date_of_finish"]),
          Description = reader["description"].ToString(),
          State = reader["state"].ToString() ?? string.Empty,
          Cost = Convert.ToDouble(reader["cost"])
        });
      }
      return orders;
    }
  }
}
