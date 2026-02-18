namespace Database.AisService.Repository;

using Database.AisServcice;
using Database.AisService.Helpers;
using Database.AisService.Models;
using Microsoft.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

internal partial class UserRepository : Repository
{

	private string GetUserActionsSelect(int countLastMounth, string nomz = "", bool toDelete = false, bool toCreate = false, bool toChangeSurname = false, bool getLastAction = true)
	{
		List<string> conditionsInSide = new List<string>();
		List<string> conditionsOutSide = new List<string>();
		List<string> conditionsToUsersMovements = new List<string>();

		try
		{
			if (!string.IsNullOrEmpty(nomz)) getLastAction = false;

			AddGetDataByNomz(ref conditionsOutSide, nomz);
			AddRelativeDateMoveCondition(ref conditionsToUsersMovements, countLastMounth);
			AddMoveTypeCondition(ref conditionsOutSide, toDelete, toCreate, toChangeSurname);
			AddGetLastAction(ref conditionsOutSide, getLastAction);

			string sqlInSideConditions = CreateSqlCondition(conditionsInSide);
			string sqlOutSideConditions = CreateSqlCondition(conditionsOutSide);
			string sqlToUsersMovementsConditions = CreateSqlCondition(conditionsToUsersMovements);

			return $@"
           WITH RankedData AS (
            SELECT
				 s.idstud as id
				 ,s.nomz as nomz
				 ,sh.Name_F as surname
				 ,concat(s.Name_I, ' ',s.Name_O) as shortname
				 ,concat(sh.idkurs, ' ', ss.facultet, ' ',gr.Name) AS группа_ручная
				 ,s.subgroup AS подгруппа
				 ,case
		  				WHEN ss.facultet LIKE 'ФЗО' THEN '+'
		  				ELSE '-'
				 END AS является_фзо
				 ,ss.spec as специальность
				 ,spec.NameRus as специализация
				 ,gr.IdGroup as groupId
				 ,ok_mt.Name as moveReason
				 ,CAST(sh.datemoveserver AS DATE) as dateMoveServer
				 ,ok_mt.IdMoveType as moveTypeId
				 ,sh.rn as rn
				 FROM (
				 	SELECT 
				 		sh.*,
				 		ROW_NUMBER() OVER (PARTITION BY idstud ORDER BY datemoveserver DESC) as rn
				 	FROM StudentHISTORY sh
				 	{sqlToUsersMovementsConditions}
				 ) sh
				JOIN Student s ON s.idstud = sh.idstud
				LEFT JOIN Ok_movetype ok_mt ON sh.IdMoveType = ok_mt.IdMoveType
				LEFT JOIN dbo.p_studentsALL ss on s.idstud = ss.idstud
				LEFT JOIN groups gr ON sh.IdGroup = gr.IdGroup
				LEFT JOIN spec  ON s.idspecz = spec.IdSpec
			{sqlInSideConditions}
			)
			SELECT 
				id,
				nomz,
				surname,
				shortname,
				группа_ручная as groups,
				подгруппа as subgroup,
				является_фзо as is_fzo,
				специальность as speciality,
				специализация as specialization,
				groupId,
				moveReason,
				dateMoveServer
			FROM RankedData
			{sqlOutSideConditions}
			ORDER BY dateMoveServer DESC;";
		}
		catch (Exception ex)
		{
			throw ex;
		}

	}

	private string GetUserActionsSelectNotFast(int countLastMounth, string nomz = "", bool toDelete = false, bool toCreate = false, bool toChangeSurname=false, bool getLastAction = true)
    {
		List<string> conditionsInSide = new List<string>();
		List<string> conditionsOutSide = new List<string>();
	    List<string> conditionsToUsersMovements = new List<string>();

		try
		{
			if (!string.IsNullOrEmpty(nomz)) getLastAction = false;

			AddGetDataByNomz(ref conditionsOutSide, nomz);
			AddRelativeDateMoveCondition(ref conditionsToUsersMovements, countLastMounth);
			AddMoveTypeCondition(ref conditionsOutSide, toDelete, toCreate, toChangeSurname);
			AddGetLastAction(ref conditionsOutSide, getLastAction);

			string sqlInSideConditions = CreateSqlCondition(conditionsInSide);
			string sqlOutSideConditions = CreateSqlCondition(conditionsOutSide);
			string sqlToUsersMovementsConditions = CreateSqlCondition(conditionsToUsersMovements);

			return $@"
           WITH RankedData AS (
            SELECT
				 s.idstud as id
				 ,s.nomz as nomz
				 ,sh.Name_F as surname
				 ,concat(left(s.Name_I,1), '.', left(s.Name_O,1), '.') as shortname
				 ,concat(sh.idkurs, ' ', f.shortname, ' ',gr.Name) AS группа_ручная
				 ,concat(s.idkurs, ' ',f.shortname,' ',ss.Группа) AS группа_АИС
				 ,s.subgroup AS подгруппа
				 ,case
		  				WHEN f.shortname LIKE 'ФЗО' THEN '+'
		  				ELSE '-'
				 END AS является_фзо
				 ,ss.Специальность as специальность
				 ,ss.Специализация as специализация
				 ,gr.IdGroup as groupId
				 ,ok_mt.Name as moveReason
				 ,CAST(sh.datemoveserver AS DATE) as dateMoveServer
				 ,ok_mt.IdMoveType as moveTypeId
				 ,sh.rn as rn
				 FROM (
				 	SELECT 
				 		sh.*,
				 		ROW_NUMBER() OVER (PARTITION BY idstud ORDER BY datemoveserver DESC) as rn
				 	FROM StudentHISTORY sh
				 	{sqlToUsersMovementsConditions}
				 ) sh
				JOIN Student s ON s.idstud = sh.idstud
				LEFT JOIN Ok_movetype ok_mt ON sh.IdMoveType = ok_mt.IdMoveType
				LEFT JOIN Facultets f ON sh.IdF = f.IdF
				LEFT JOIN dbo.V_students_list ss on s.idstud = ss.idstud
				LEFT JOIN groups gr ON sh.IdGroup = gr.IdGroup
			{sqlInSideConditions}
			)
			SELECT 
				id,
				nomz,
				surname,
				shortname,
				группа_АИС as groups,
				подгруппа as subgroup,
				является_фзо as is_fzo,
				специальность as speciality,
				специализация as specialization,
				groupId,
				moveReason,
				dateMoveServer
			FROM RankedData
			{sqlOutSideConditions}
			ORDER BY dateMoveServer DESC;";
		}
		catch (Exception ex) 
		{
			throw ex;
		}
		
	}

	
	private static string CreateSqlCondition(List<string> conditions)
	{
		if (conditions.Count() == 0) return "";
		return " WHERE " + string.Join(" AND ", conditions);
	}

	private void AddGetDataByNomz(ref List<string> conditionsOutSide, string nomz)
	{
		if (string.IsNullOrEmpty(nomz)) return;
		if(nomz.Length<6)throw new NullReferenceException("Номер зачетной книги не может быть меньше 6 символов. ");
		conditionsOutSide.Add("nomz like '" + nomz + "'");

	}

	private static void AddGetLastAction(ref List<string> conditions, bool getLastAction)
	{
		if (!getLastAction) return;
		conditions.Add("rn = 1");
	}
	private static void AddRelativeDateMoveCondition(ref List<string> conditions, int countLastMounth)
	{
		if (countLastMounth < 1) throw new Exception("Not correct date");
		if (countLastMounth == 0) return;

		string condition = $@" 
			(
				DATEADD(MONTH, {countLastMounth}, CAST(sh.datemoveserver AS DATE)) > CAST(GETDATE() AS DATE) 
				OR 
				DATEADD(MONTH, {countLastMounth}, CAST(sh.datemove AS DATE)) > CAST(GETDATE() AS DATE)
			)";
		conditions.Add(condition);
	}

	private static void AddMoveTypeCondition(ref List<string> conditions, bool toDelete, bool toCreate, bool toChangeSurname)
	{
		var conditionMoveType = GetMoveTypeCondition(toDelete, toCreate, toChangeSurname);
		if (string.IsNullOrEmpty(conditionMoveType)) return;

		conditions.Add(conditionMoveType);
	}

	private static string GetMoveTypeCondition(bool toDelete, bool toCreate, bool toChangeSurname)
	{
		List<string> conditionMoveType = new List<string>();
		if (toDelete)
		{
			conditionMoveType.Add("14,17,20,104,106"); // выпуск, академ, отчислить
		}
		if (toCreate)
		{
			conditionMoveType.Add("4,11,12,13,18,103,105,106,112"); // восстановление, перевод, зачисление
		}
		if (toChangeSurname)
		{
			conditionMoveType.Add("16"); // изменить фамилию
		}

		if (conditionMoveType.Count() == 0) return "";

		return "moveTypeId IN (" + string.Join(",", conditionMoveType) + ")";
	}

}
