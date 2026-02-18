using Database.MoodleService.Models;
using Database.MoodleService.Helpers;

namespace Database.MoodleService.Repository;

internal class UserRepository : RepositoryBase, IDisposable
{
	private readonly MariaDbConnector _dbConnector;

	internal UserRepository(MariaDbConnector? dbConnector = null)
	{
		_dbConnector = dbConnector ?? new MariaDbConnector();
	}

	internal async Task<List<User>> GetUsersAsync(bool withUtilsGroups = true)
	{
		string additionalSqlConditions = "";
		if (!withUtilsGroups)
		{
			additionalSqlConditions += $@"
			LEFT JOIN moodle.mdl_cohort_members cm ON cm.userid = u.id 
			LEFT JOIN moodle.mdl_cohort coh ON coh.id = cm.cohortid
			WHERE 
				(coh.name is null OR coh.name NOT IN ('Учебная','Заблокированные','ОИВР','ЦООД','Деканы','Служебная','Олимпиада','Зав. кафедрами','МКиТП'))
				and u.deleted = 0
			GROUP BY u.id, u.username, u.lastname, u.firstname
			";

		}
		additionalSqlConditions = !string.IsNullOrEmpty(additionalSqlConditions) ? "WHERE " + additionalSqlConditions : "";

		string sqlExpression = $@"
            SELECT u.id, u.username, u.lastname, u.firstname, ''
            FROM moodle.mdl_user u
			{additionalSqlConditions}
            ORDER BY u.lastaccess";


		var results = _dbConnector.RunDatabaseScript(sqlExpression);

		return results.Select(ConvertToUser).ToList();
	}

	private static User ConvertToUser(object tuple)
	{
		try
		{
			var (id, username, lastname, firstname,group) = ExtractTupleValuesToUser(tuple);

			return new User
			{
				Id = ParseInt(id),
				UserName = ParseString(username),
				Surname = ParseString(lastname),
				FirstName = ParseString(firstname),
				Group = ParseString(group)
			};
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Failed to convert tuple to User: {ex.Message}", ex);
		}
	}




	public void Dispose()
	{
		_dbConnector?.Dispose();
	}
}