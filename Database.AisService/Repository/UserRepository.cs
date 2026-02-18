namespace Database.AisService.Repository;

using Database.AisServcice;
using Database.AisService.Helpers;
using Database.AisService.Models;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

internal partial class UserRepository : Repository
{
	internal async Task<List<User>> GetLastUserActions(int countRelativeMounth, string nomz)
	{
		AisDbConnector db;

		string sqlExpression = GetUserActionsSelect(countRelativeMounth, getLastAction: true, nomz:nomz);

		db = new AisDbConnector();
		var objects = await db.RunDatabaseScriptAsyncReturnsDictionary(sqlExpression);

		return objects
			.Select(ConvertToUserByDictionary)
			.ToList();
	}

	internal async Task<List<User>> GetLastUsersActions(int countRelativeMounth) 
	{
		AisDbConnector db;

		string sqlExpression = GetUserActionsSelect(countRelativeMounth , getLastAction:true);

		db = new AisDbConnector();
		var objects = await db.RunDatabaseScriptAsyncReturnsDictionary(sqlExpression);

		return objects
			.Select(ConvertToUserByDictionary)
			.ToList();
	}
	internal async Task<List<User>> GetLastUsersActions(int countRelativeMounth, bool toDelete = false, bool toCreate = false)
	{
		AisDbConnector db;

		string sqlExpression = GetUserActionsSelect(countRelativeMounth, toDelete:toDelete, toCreate:toCreate);

		db = new AisDbConnector();
		var objects = await db.RunDatabaseScriptAsyncReturnsDictionary(sqlExpression);

		return objects
			.Select(ConvertToUserByDictionary)
			.ToList();

	}
	internal async Task<List<User>> GetLastUsersActions(int countRelativeMounth, bool toChangeSurname = false)
	{
		AisDbConnector db;

		string sqlExpression = GetUserActionsSelect(countRelativeMounth, toChangeSurname:toChangeSurname, getLastAction:false);

		db = new AisDbConnector();
		var objects = await db.RunDatabaseScriptAsyncReturnsDictionary(sqlExpression);

		return objects
			.Select(ConvertToUserByDictionary)
			.ToList();

	}


	private static User ConvertToUserByDictionary(Dictionary<string,object> row)
	{
		try
		{
			return new User
			{
				Id = ParseInt(row["id"]),
				NomZ = ParseString(row["nomz"]).ToLower().Replace("у", "").Replace("м", "").Replace("m", "").Replace("а", ""),
				Surname = ParseString(row["surname"]),
				ShortName = ParseString(row["shortname"]),
				Group = ParseString(row["groups"]),
				GroupId = ParseString(row["groupId"]),
				MoveReason = ParseString(row["moveReason"]),
				Datemove = ParseDate(row["dateMoveServer"]),

				Subgroup = ParseString(row["subgroup"]),
				IsFzo = ParseString(row["is_fzo"]),
				Speciality = ParseString(row["speciality"]),
				Specialization = ParseString(row["specialization"]),

			};
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Failed to convert tuple to User: {ex.Message}", ex);
		}
	}
	private static User ConvertToUser(object tuple)
	{
		try
		{
			//var tup = tuple as ITuple;
			//var res = tup.ToArray();


			var res = ExtractNamedTupleValues(tuple);

			var (id, nomz, surname, shortname, group, groupId, movereason, datemove) = ExtractTupleValues<int, string, string, string, string, int, string, DateTime>(tuple);

			var dat = ParseDate(datemove);
			return new User
			{
				NomZ = ParseString(nomz).ToLower().Replace("у", "").Replace("м", "").Replace("m", "").Replace("а", ""),
				Surname = ParseString(surname),
				ShortName = ParseString(shortname),
				Group = ParseString(group),
				GroupId = ParseString(groupId),
				MoveReason = ParseString(movereason),
				Datemove = ParseDate(datemove)
			};
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Failed to convert tuple to User: {ex.Message}", ex);
		}
	}
}
