using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vistest.Models;

namespace vistest.DataServices
{
  public class ServisRepository(DbService dbService) : BaseRepository(dbService)
  {

    public int Add(Servis servis)
    {
      string query = @"
                INSERT INTO Servis (name, address, opened_at, closed_at)
                VALUES (@Name, @Address, @OpenedAt, @ClosedAt);";

      var parameters = new Dictionary<string, object?>
            {
                {"@Name", servis.Name},
                {"@Address", servis.Address},
                {"@OpenedAt", servis.OpenedAt},
                {"@ClosedAt", servis.ClosedAt}
            };

      return ExecuteScalarInsert(query, parameters);
    }

    public void Update(Servis servis)
    {
      string query = @"
                UPDATE Servis
                SET name = @Name, address = @Address, opened_at = @OpenedAt, closed_at = @ClosedAt
                WHERE id_servis = @Id;";

      var parameters = new Dictionary<string, object?>
            {
                {"@Id", servis.Id},
                {"@Name", servis.Name},
                {"@Address", servis.Address},
                {"@OpenedAt", servis.OpenedAt},
                {"@ClosedAt", servis.ClosedAt}
            };

      ExecuteNonQuery(query, parameters);
    }

    public void Delete(int id)
    {
      string query = "DELETE FROM Servis WHERE id_servis = @Id;";
      var parameters = new Dictionary<string, object?> { { "@Id", id } };
      ExecuteNonQuery(query, parameters);
    }

    public Servis? Get(int id)
    {
      string query = "SELECT * FROM Servis WHERE id_servis = @Id;";
      var parameters = new Dictionary<string, object?> { { "@Id", id } };

      using var reader = ExecuteReader(query, parameters);
      if (reader.Read())
      {
        return new Servis
        {
          Id = Convert.ToInt32(reader["id_servis"]),
          Name = reader["name"].ToString() ?? string.Empty,
          Address = reader["address"].ToString() ?? string.Empty,
          OpenedAt = Convert.ToDateTime(reader["opened_at"]),
          ClosedAt = reader["closed_at"] == DBNull.Value ? null : Convert.ToDateTime(reader["closed_at"])
        };
      }
      return null;
    }

    public List<Servis> GetAll()
    {
      var services = new List<Servis>();
      string query = "SELECT * FROM Servis;";

      using var reader = ExecuteReader(query);
      while (reader.Read())
      {
        services.Add(new Servis
        {
          Id = Convert.ToInt32(reader["id_servis"]),
          Name = reader["name"].ToString() ?? string.Empty,
          Address = reader["address"].ToString() ?? string.Empty,
          OpenedAt = Convert.ToDateTime(reader["opened_at"]),
          ClosedAt = reader["closed_at"] == DBNull.Value ? null : Convert.ToDateTime(reader["closed_at"])
        });
      }
      return services;
    }
  }
}
