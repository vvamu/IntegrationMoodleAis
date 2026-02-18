namespace Database.AisService.Repository;

using Database.AisServcice;
using Database.AisService.Helpers;
using Database.AisService.Models;
using Microsoft.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

internal partial class GroupRepository : Repository
{
	internal string GetUserByIdWithСlassmatesSelect(string nomZ)
	{
		try
		{
			return $@"
				SELECT TOP 30 * FROM Students.dbo.V_students_lists WHERE idgroup = (
					SELECT idgroup FROM Students.dbo.V_students_lists WHERE nomz like '{nomZ}'
				)
				ORDER BY name_f;
			";

		}
		catch (Exception ex)
		{
			throw ex;
		}

	}

	private string GetUsersByGroupIdSelect(int groupId)
    {
		try
		{
			return $@"SELECT TOP 30 * FROM Students.dbo.V_students_lists WHERE idgroup = {groupId};";
		}
		catch (Exception ex) 
		{
			throw ex;
		}
	}


}
