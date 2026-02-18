namespace Database.AisService.Repository;

using Database.AisServcice;
using Database.AisService.Helpers;
using Database.AisService.Models;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;


internal partial class GroupRepository : Repository
{
	internal async Task<Object> GetUsersByGroupId(int groupId)
	{
		AisDbConnector db;

		string sqlExpression = GetUsersByGroupIdSelect(groupId);

		db = new AisDbConnector();
		var objects = await db.RunDatabaseScriptAsync(sqlExpression);

		return objects;
	}

	internal async Task<Object> GetUserByNomzWithСlassmates(string nomZ)
	{
		AisDbConnector db;

		string sqlExpression = GetUserByIdWithСlassmatesSelect(nomZ);

		db = new AisDbConnector();
		var objects = await db.RunDatabaseScriptAsync(sqlExpression);

		return objects;
	}

}
