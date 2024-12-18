using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.DataServices
{
  public class DbService
  {
    private readonly string _dbPath;
    private readonly string _connectionString;

    public DbService(string dbFileName = "database.db")
    {
      _dbPath = Path.Combine(FileSystem.AppDataDirectory, dbFileName);
      _connectionString = $"Data Source={_dbPath};Version=3;";
      InitializeDatabase();
    }

    private void InitializeDatabase()
    {
      if (!File.Exists(_dbPath))
      {
        SQLiteConnection.CreateFile(_dbPath);
      }

      using (var connection = new SQLiteConnection(_connectionString))
      {
        connection.Open();

        var createTablesScript = @"
                    CREATE TABLE IF NOT EXISTS Customer (
                        id_customer INTEGER PRIMARY KEY,
                        name VARCHAR(45) NOT NULL,
                        surname VARCHAR(45) NOT NULL,
                        phone VARCHAR(13),
                        email VARCHAR(45)
                    );

                    CREATE TABLE IF NOT EXISTS Car (
                        id_car INTEGER PRIMARY KEY,
                        id_customer INTEGER NOT NULL,
                        brand VARCHAR(45) NOT NULL,
                        model VARCHAR(45) NOT NULL,
                        licence_plate VARCHAR(17),
                        last_mileage INTEGER,
                        FOREIGN KEY (id_customer) REFERENCES Customer (id_customer)
                    );

                    CREATE TABLE IF NOT EXISTS Servis (
                        id_servis INTEGER PRIMARY KEY,
                        name VARCHAR(45) NOT NULL,
                        address VARCHAR(100) NOT NULL,
                        opened_at DATE NOT NULL,
                        closed_at DATE
                    );

                    CREATE TABLE IF NOT EXISTS Employee (
                        id_employee INTEGER PRIMARY KEY,
                        id_servis INTEGER NOT NULL,
                        name VARCHAR(45) NOT NULL,
                        surname VARCHAR(45) NOT NULL,
                        position VARCHAR(45) NOT NULL,
                        employment_start_at DATE NOT NULL,
                        employment_end_at DATE,
                        salary DOUBLE NOT NULL,
                        FOREIGN KEY (id_servis) REFERENCES Servis (id_servis)
                    );

                    CREATE TABLE IF NOT EXISTS Part (
                        id_part INTEGER PRIMARY KEY,
                        brand VARCHAR(45) NOT NULL,
                        model VARCHAR(45) NOT NULL,
                        description VARCHAR(100)
                    );

                    CREATE TABLE IF NOT EXISTS Part_Servis (
                        id_part INTEGER NOT NULL,
                        id_servis INTEGER NOT NULL,
                        stock INTEGER NOT NULL,
                        PRIMARY KEY (id_part, id_servis),
                        FOREIGN KEY (id_part) REFERENCES Part (id_part),
                        FOREIGN KEY (id_servis) REFERENCES Servis (id_servis)
                    );

                    CREATE TABLE IF NOT EXISTS `Order` (
                        id_order INTEGER PRIMARY KEY,
                        id_car INTEGER NOT NULL,
                        id_servis INTEGER NOT NULL,
                        created_at DATETIME NOT NULL,
                        date_of_start DATETIME,
                        date_of_finish DATETIME,
                        description TEXT,
                        state VARCHAR(20),
                        cost DOUBLE,
                        FOREIGN KEY (id_car) REFERENCES Car (id_car),
                        FOREIGN KEY (id_servis) REFERENCES Servis (id_servis)
                    );";

        using (var command = new SQLiteCommand(createTablesScript, connection))
        {
          command.ExecuteNonQuery();
        }
      }
    }

    public SQLiteConnection GetConnection()
    {
      return new SQLiteConnection(_connectionString);
    }
  }
}
