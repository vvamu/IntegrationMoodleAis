namespace Database.AisServcice;

using Database.AisService.Models;
using Microsoft.Data.SqlClient;
using System.Runtime;
using System.Runtime.CompilerServices;

internal class AisDbConnector : Configuration
{
	internal async Task<List<Object>> RunDatabaseScriptAsync(string sqlExpression)
	{
		if (string.IsNullOrEmpty(sqlExpression)) throw new Exception("Sql expression not set");

		await RunDatabaseConnection();
		await using var command = new SqlCommand(sqlExpression, _sqlServersConnection);
		await using var reader = await command.ExecuteReaderAsync();

		var list = new List<object>();

		var columnNames = new string[reader.FieldCount];
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

	internal async Task<List<Dictionary<string, object>>> RunDatabaseScriptAsyncReturnsDictionary(string sqlExpression)
	{
		if (string.IsNullOrEmpty(sqlExpression)) throw new Exception("Sql expression not set");

		await RunDatabaseConnection();
		await using var command = new SqlCommand(sqlExpression, _sqlServersConnection);
		await using var reader = await command.ExecuteReaderAsync();

		var result = new List<Dictionary<string, object>>();

		var columns = Enumerable.Range(0, reader.FieldCount)
		.Select(reader.GetName)
		.ToArray();

		while (reader.Read())
		{
			var dict = columns
				.Select((col, idx) => new { col, value = reader[idx] })
				.ToDictionary(x => x.col, x => x.value == DBNull.Value ? null : x.value);

			result.Add(dict);
		}
		return result;
	}

	private static ITuple CreateTuple(object[] values)
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
}
