using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.DataServices
{
  public class PartServisRepository(DbService dbService) : BaseRepository(dbService)
  {
    public void Add(int idPart, int idServis, int stock)
    {
      string query = @"
                INSERT INTO Part_Servis (id_part, id_servis, stock)
                VALUES (@IdPart, @IdServis, @Stock);";

      var parameters = new Dictionary<string, object?>
            {
                {"@IdPart", idPart},
                {"@IdServis", idServis},
                {"@Stock", stock}
            };

      ExecuteNonQuery(query, parameters);
    }

    public void Update(int idPart, int idServis, int stock)
    {
      string query = @"
                UPDATE Part_Servis
                SET stock = @Stock
                WHERE id_part = @IdPart and id_servis =  @IdServis;";

      var parameters = new Dictionary<string, object?>
            {
                {"@IdPart", idPart},
                {"@IdServis", idServis},
                {"@Stock", stock}
            };

      ExecuteNonQuery(query, parameters);
    }

    public void Delete(int idPart, int idServis)
    {
      string query = "DELETE FROM Part_Servis WHERE id_part = @IdPart and id_servis =  @IdServis;";
      var parameters = new Dictionary<string, object?> 
          {
              {"@IdPart", idPart},
              {"@IdServis", idServis}
          };

      ExecuteNonQuery(query, parameters);
    }

    public int? Get(int idPart, int idServis)
    {
      string query = "SELECT * FROM Part_Servis WHERE id_part_servis = @Id;";
      var parameters = new Dictionary<string, object?> 
          {
              {"@IdPart", idPart},
              {"@IdServis", idServis}
          };

      using var reader = ExecuteReader(query, parameters);
      if (reader.Read())
      {
        return Convert.ToInt32(reader["stock"]);
      }
      return null;
    }
  }
}
