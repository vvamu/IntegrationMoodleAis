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
		string sqlExpression = GetUserActionsSelect(countRelativeMounth, getLastAction: true, nomz:nomz);

		db = new AisDbConnector();
		var objects = await db.RunDatabaseScriptAsyncReturnsDictionary(sqlExpression);

		return objects
			.Select(ConvertToUserByDictionary)
			.ToList();
	}

	internal async Task<List<User>> GetLastUsersActions(int countRelativeMounth) 
	{
		string sqlExpression = GetUserActionsSelect(countRelativeMounth , getLastAction:true);

		db = new AisDbConnector();
		var objects = await db.RunDatabaseScriptAsyncReturnsDictionary(sqlExpression);

		return objects
			.Select(ConvertToUserByDictionary)
			.ToList();
	}
	internal async Task<List<User>> GetLastUsersActions(int countRelativeMounth, bool toDelete = false, bool toCreate = false)
	{
		string sqlExpression = GetUserActionsSelect(countRelativeMounth, toDelete:toDelete, toCreate:toCreate);

		db = new AisDbConnector();
		var objects = await db.RunDatabaseScriptAsyncReturnsDictionary(sqlExpression);

		return objects
			.Select(ConvertToUserByDictionary)
			.ToList();

	}
	internal async Task<List<User>> GetLastUsersActions(int countRelativeMounth, bool toChangeSurname = false)
	{
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
				NomZ = ParseString(row["nomz"]).ToLower().Replace("у", "").Replace("м", "").Replace("m", "").Replace("а", "").Replace("a", ""),
				Surname = ParseString(row["surname"]),
				ShortName = ConvertFirstNameToShortName(ParseString(row["name"]), ParseString(row["patronymic"])) ,
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

	private static string ConvertFirstNameToShortName(string name,string patronymic)
	{
		if (string.IsNullOrEmpty(name)) return "";
		if(string.IsNullOrEmpty(patronymic)) return name;
		return $"{name.ToUpper()[0]}.{patronymic.ToUpper()[0]}.";
	}
}
