using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vistest.Models;

namespace vistest.DataServices
{
  public class PartRepository(DbService service) : BaseRepository(service)
  {
    public void Add(Part part)
    {
      string query = @"
                INSERT INTO Part (brand, model, description)
                VALUES (@Brand, @Model, @Description);";

      var parameters = new Dictionary<string, object?>
            {
                {"@Brand", part.Brand},
                {"@Model", part.Model},
                {"@Description", part.Description}
            };

      ExecuteNonQuery(query, parameters);
    }

    public void Update(Part part)
    {
      string query = @"
                UPDATE Part
                SET brand = @Brand, model = @Model, description = @Description
                WHERE id_part = @Id;";

      var parameters = new Dictionary<string, object?>
            {
                {"@Id", part.Id},
                {"@Brand", part.Brand},
                {"@Model", part.Model},
                {"@Description", part.Description}
            };

      ExecuteNonQuery(query, parameters);
    }

    public void Delete(int id)
    {
      string query = "DELETE FROM Part WHERE id_part = @Id;";
      var parameters = new Dictionary<string, object?> { { "@Id", id } };
      ExecuteNonQuery(query, parameters);
    }

    public Part? Get(int id)
    {
      string query = "SELECT * FROM Part WHERE id_part = @Id;";
      var parameters = new Dictionary<string, object?> { { "@Id", id } };

      using var reader = ExecuteReader(query, parameters);
      if (reader.Read())
      {
        return new Part
        {
          Id = Convert.ToInt32(reader["id_part"]),
          Brand = reader["brand"].ToString() ?? string.Empty,
          Model = reader["model"].ToString() ?? string.Empty,
          Description = reader["description"].ToString() ?? string.Empty
        };
      }
      return null;
    }

    public List<Part> GetAll()
    {
      var parts = new List<Part>();
      string query = "SELECT * FROM Part;";

      using var reader = ExecuteReader(query);
      while (reader.Read())
      {
        parts.Add(new Part
        {
          Id = Convert.ToInt32(reader["id_part"]),
          Brand = reader["brand"].ToString() ?? string.Empty,
          Model = reader["model"].ToString() ?? string.Empty,
          Description = reader["description"].ToString() ?? string.Empty
        });
      }
      return parts;
    }
  }
}
