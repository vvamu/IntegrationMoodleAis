namespace Database.AisService.Repository;

using Database.AisServcice;
using Database.AisService.Models;


internal class UserRepository : RepositoryBase
{
	internal async Task<List<User>> GetUsers()
    {
		var db = new AisDbConnector();

		const string sqlExpression = @"
            SELECT
				 s.nomz 
				 ,REPLACE(REPLACE(REPLACE(REPLACE(s.nomz,'У',''),'M',''),'М',''),'А','')
				  ,ok_mt.Name
				 ,CAST(sh.datemoveserver AS DATE) Datemoveserver
			FROM StudentHISTORY sh 
				JOIN Student s ON s.idstud = sh.idstud
				LEFT JOIN Ok_movetype ok_mt ON sh.IdMoveType = ok_mt.IdMoveType
			WHERE 
			(
				DATEADD(MONTH, 2, CAST(sh.datemoveserver AS DATE)) > CAST(GETDATE() AS DATE) 
				OR 
				DATEADD(MONTH, 2, CAST(sh.datemove AS DATE)) > CAST(GETDATE() AS DATE)
			)
			AND 
			  sh.IdMoveType IN (14,17,20,104,106) --выпуск, академ, отчислить
			Order BY datemoveserver DESC;";

		var objects = await db.RunDatabaseScriptAsync(sqlExpression);

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
				NomZ = ParseString(id),
				LastName = ParseString(username),
				MoveReason = ParseString(lastname),
				Datemove = ParseDate(firstname)
			};
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException($"Failed to convert tuple to User: {ex.Message}", ex);
		}
	}
}
