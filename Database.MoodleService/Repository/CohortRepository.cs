using Database.MoodleService.Helpers;
using System.Xml.Linq;

namespace Database.MoodleService.Models;

internal class CohortRepository: RepositoryBase
{
	private readonly MariaDbConnector _dbConnector;

	internal CohortRepository(MariaDbConnector? dbConnector = null)
	{
		_dbConnector = dbConnector ?? new MariaDbConnector();
	}

	internal async Task<List<Cohort>> GetCohortsAsync(bool withUtilsGroups = true)
	{
		string additionalSqlConditions = "";
		additionalSqlConditions = !string.IsNullOrEmpty(additionalSqlConditions) ? "WHERE " + additionalSqlConditions : "";

		if (!withUtilsGroups)
		{
			additionalSqlConditions += $@"
			(coh.name is null OR coh.name NOT IN('Учебная', 'Заблокированные', 'ОИВР', 'ЦООД', 'Деканы', 'Служебная', 'Олимпиада', 'Зав. кафедрами', 'МКиТП')) 
			";
		}

		string sqlExpression = $@"
            SELECT coh.id, coh.name
            FROM moodle.mdl_cohort coh
			{additionalSqlConditions}
            ORDER BY coh.timecreated";


		var results = _dbConnector.RunDatabaseScript(sqlExpression);

		return results.Select(ConvertToCohort).ToList();
	}

	internal async Task<List<Cohort>> GetCohortsByNameAsync(string cohortNamePart)
	{
		string additionalSqlConditions = $@"lower(coh.name) like '%{cohortNamePart.ToLower()}%";

		additionalSqlConditions = !string.IsNullOrEmpty(additionalSqlConditions) ? "WHERE " + additionalSqlConditions : "";


		string sqlExpression = $@"
            SELECT coh.id, coh.name
            FROM moodle.mdl_cohort coh
			{additionalSqlConditions}
            ORDER BY coh.timecreated";


		var results = _dbConnector.RunDatabaseScript(sqlExpression);

		return results.Select(ConvertToCohort).ToList();
	}

	private static Cohort ConvertToCohort(object tuple)
	{
		try
		{
			var (id, name) = ExtractTupleValuesToCohort(tuple);

			return new Cohort
			{
				Id = ParseInt(id),
				Name = ParseString(name)
			};
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Failed to convert tuple to Cohort: {ex.Message}", ex);
		}
	}
}