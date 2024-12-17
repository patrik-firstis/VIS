using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Vzory._3_RelacneObjektoveChovani
{
	[Obsolete]
	public class UnitOfWork
	{
		private readonly SqlConnection _connection;
		private SqlTransaction _transaction;
		private List<SqlCommand> _commands = new List<SqlCommand>();

		public UnitOfWork(SqlConnection connection)
		{
			_connection = connection;
			_connection.Open();
		}

		// Začiatok transakcie
		public void BeginTransaction()
		{
			_transaction = _connection.BeginTransaction();
		}

		public void Commit()
		{
			_transaction?.Commit();
		}

		public void AddCommand(string commandText, params SqlParameter[] parameters)
		{
			var command = new SqlCommand(commandText, _connection, _transaction);
			command.Parameters.AddRange(parameters);
			_commands.Add(command);
		}

		public void Rollback()
		{
			_transaction?.Rollback();
		}
	}
	[Obsolete]
	public class Test3
	{
		public void Test()
		{
			var connection = new SqlConnection("Server=.;Database=Test;Trusted_Connection=True;");
			var unitOfWork = new UnitOfWork(connection);
			unitOfWork.BeginTransaction();

			// Do some work

			unitOfWork.Commit();
		}
	}
}