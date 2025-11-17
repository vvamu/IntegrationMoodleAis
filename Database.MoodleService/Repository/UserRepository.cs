using Database.MoodleService.Models;
using Database.MoodleService.Helpers;

namespace Database.MoodleService.Repository;

internal class UserRepository : Repository, IDisposable
{
	private readonly MariaDbConnector _dbConnector;

	internal UserRepository(MariaDbConnector? dbConnector = null)
	{
		_dbConnector = dbConnector ?? new MariaDbConnector();
	}

	internal async Task<List<User>> GetUsersAsync()
	{
		const string sqlExpression = @"
            SELECT u.id, u.username, u.lastname, u.firstname 
            FROM moodle.mdl_user u 
            ORDER BY u.lastaccess";

		var results = await Task.Run(() => _dbConnector.RunDatabaseScript(sqlExpression));

		return results.Select(ConvertToUser).ToList();
	}

	internal List<User> GetUsers()
	{
		const string sqlExpression = @"
            SELECT u.id, u.username, u.lastname, u.firstname 
            FROM moodle.mdl_user u 
				JOIN moodle.mdl_cohort_members cm ON cm.userid = u.id 
				JOIN moodle.mdl_cohort coh ON coh.id = cm.cohortid
			WHERE coh.name NOT IN ('Учебная','Заблокированные','ОИВР','ЦООД','Деканы','Служебная','Олимпиада','Зав. кафедрами','МКиТП') 
            ORDER BY u.lastaccess";

		var objects = _dbConnector.RunDatabaseScript(sqlExpression);
		return objects
			.Select(ConvertToUser)
			.ToList();
	}


	private static User ConvertToUser(object tuple)
	{
		try
		{
			var (id, username, lastname, firstname) = ExtractTupleValues(tuple);

			return new User
			{
				Id = ParseInt(id),
				UserName = ParseString(username),
				LastName = ParseString(lastname),
				FirstName = ParseString(firstname)
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