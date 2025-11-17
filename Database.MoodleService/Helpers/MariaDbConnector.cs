namespace Database.MoodleService.Helpers;

using Database.MoodleService.Helpers;
using Database.MoodleService.Models;
using MySqlConnector;
using Renci.SshNet;
using System.Runtime.CompilerServices;
using static Org.BouncyCastle.Math.EC.ECCurve;


//1. proxy
//2. к серверу
//3. к бд
internal class MariaDbConnector : IDisposable
{
	private MySqlConnection _mySqlConnection;

	internal List<object> RunDatabaseScript(string sqlExpression = "")
	{
		if (string.IsNullOrEmpty(sqlExpression)) throw new Exception("Sql expression not set");
		return RunSql(sqlExpression);
	}
	private List<object> RunSql(string sqlExpression)
	{
		using (var config = new Configuration())
		{
			var sshClient = config.SetSshConnection();
			if (sshClient == null)
			{
				throw new Exception("Failed to establish SSH connection");
			}

			_mySqlConnection = config.RunDatabaseConnection();
			return RunSqlExpression(sqlExpression);

		}
	}
	private List<object>? RunSqlExpression(string sqlExpression)
	{
		if (string.IsNullOrEmpty(sqlExpression)) return null;

		using var command = new MySqlCommand(sqlExpression, _mySqlConnection);
		using var reader = command.ExecuteReader();

		var list = new List<object>();
		
		var columnNames = new string[reader.FieldCount]; // Get column names first (assuming they remain constant)
		for (int i = 0; i < reader.FieldCount; i++)
		{
			columnNames[i] = reader.GetName(i);
		}

		while (reader.Read())
		{
			var values = new object[reader.FieldCount];
			for (int i = 0; i < reader.FieldCount; i++)
			{
				values[i] = reader[i];
			}
			list.Add(CreateTuple(values));
		}

		return list;
	}


	static ITuple CreateTuple(object[] values)
	{
		return values.Length switch
		{
			1 => ValueTuple.Create(values[0]),
			2 => ValueTuple.Create(values[0], values[1]),
			3 => ValueTuple.Create(values[0], values[1], values[2]),
			4 => ValueTuple.Create(values[0], values[1], values[2], values[3]),
			5 => ValueTuple.Create(values[0], values[1], values[2], values[3], values[4]),
			6 => ValueTuple.Create(values[0], values[1], values[2], values[3], values[4], values[5]),
			7 => ValueTuple.Create(values[0], values[1], values[2], values[3], values[4], values[5], values[6]),
			8 => ValueTuple.Create(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7]),
			_ => throw new NotSupportedException($"Tuples with {values.Length} elements are not supported")
		};
	}

    public void Dispose()
    {
		

	}
}
