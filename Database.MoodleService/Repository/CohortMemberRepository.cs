using Database.MoodleService.Helpers;

namespace Database.MoodleService.Models;

internal class CohortMemberRepository : RepositoryBase
{
	private readonly MariaDbConnector _dbConnector;

	internal CohortMemberRepository(MariaDbConnector? dbConnector = null)
	{
		_dbConnector = dbConnector ?? new MariaDbConnector();
	}

	internal async Task<List<CohortMember>> GetUsersByCohortId(int cohortId)
	{
		string additionalSqlConditions = "";
		additionalSqlConditions = !string.IsNullOrEmpty(additionalSqlConditions) ? "WHERE " + additionalSqlConditions : "";

		string sqlExpression = $@"
            SELECT c.id, c.cohortid, c.userid
            FROM moodle.mdl_cohort_members c
			${additionalSqlConditions}
            ORDER BY c.timeadded";

		var results = _dbConnector.RunDatabaseScript(sqlExpression);

		return results.Select(ConvertToCohortMember).ToList();
	}

	private static CohortMember ConvertToCohortMember(object tuple)
	{
		try
		{
			var (id, cohortId,userId) = ExtractTupleValuesToCohortMembers(tuple);

			return new CohortMember
			{
				Id = ParseInt(id),
				CohortId = ParseInt(cohortId),
				UserId = ParseInt(userId)
			};
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Failed to convert tuple to Cohort: {ex.Message}", ex);
		}
	}
}