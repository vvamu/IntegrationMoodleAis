using Database.AisServcice;
using Database.AisService.Enums;
using Database.AisService.Models;
using System.Runtime.CompilerServices;

namespace Database.AisService.Helpers;

internal class Repository 
{
	protected AisDbConnector db;
	internal async Task<List<Dictionary<string, object>>> GetTableAsync(AisTable table) 
		=> await GetAllRowsByTableAsync(table.ToString());
	protected static int ParseInt(object value) =>
	Convert.ToInt32(value ?? throw new ArgumentNullException(nameof(value)));

	protected static string ParseString(object value) =>
		Convert.ToString(value) ?? string.Empty;

	protected static DateTime ParseDate(object value) =>
		Convert.ToDateTime(value);

	protected async Task<List<Dictionary<string,object>>> GetAllRowsByTableAsync(string tableName)
	{
		string sqlExpression = $"SELECT * FROM {tableName}";
		db = new AisDbConnector();
		return await db.RunDatabaseScriptAsyncReturnsDictionary(sqlExpression);
	}
}