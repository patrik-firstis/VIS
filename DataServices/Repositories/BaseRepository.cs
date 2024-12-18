using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.DataServices
{
  public abstract class BaseRepository
  {
    protected readonly DbService dbService;

    protected BaseRepository(DbService dbService)
    {
      this.dbService = dbService;
    }

    protected void ExecuteNonQuery(string query, Dictionary<string, object?> parameters)
    {
      using var command = new SQLiteCommand(query, dbService.GetConnection());
      foreach (var param in parameters)
      {
        command.Parameters.AddWithValue(param.Key, param.Value);
      }
      command.ExecuteNonQuery();
    }

    protected SQLiteDataReader ExecuteReader(string query, Dictionary<string, object?>? parameters = null)
    {
      var command = new SQLiteCommand(query, dbService.GetConnection());
      if (parameters != null)
      {
        foreach (var param in parameters)
        {
          command.Parameters.AddWithValue(param.Key, param.Value);
        }
      }
      return command.ExecuteReader();
    }
  }
}
