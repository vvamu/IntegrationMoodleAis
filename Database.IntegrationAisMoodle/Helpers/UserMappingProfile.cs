using CsvHandler.Models;
using Database.MoodleService.Models;
using IntegrationAisMoodle.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
public static class UserMappingProfile
{

	public static UserViewModel ToViewModel(Database.AisService.Models.User? aisUser, Database.MoodleService.Models.User? moodleUser = null)
	{
		return new UserViewModel
		{
			AisNomZ = aisUser?.NomZ,
			AisSurname = aisUser?.Surname,
			AisFirstName = aisUser?.ShortName,
			AisMoveReason = aisUser?.MoveReason,
			AisGroup = aisUser?.Group,
			AisGroupId = aisUser?.GroupId,
			AisDateMoveServer = aisUser?.Datemove ?? default,

			MoodleId = moodleUser?.Id ?? 0,
			MoodleUserName = moodleUser?.UserName,
			MoodleSurname = moodleUser?.Surname,
			MoodleFirstName = moodleUser?.FirstName,
			MoodleGroup = moodleUser?.Group
		};
	}

	public static CsvCreateElement ToCsvViewModel(Database.AisService.Models.User? aisUser)
	{	
		return new CsvCreateElement(username: aisUser?.NomZ, lastname: aisUser.Surname, firstname: aisUser.ShortName, cohort: aisUser.Group);
	}
	public static CsvCreateElement ToCsvViewModel(UserViewModel? aisUser)
	{
		return new CsvCreateElement(username: aisUser?.AisNomZ, lastname: aisUser.AisSurname, firstname: aisUser.AisFirstName, cohort: aisUser.AisGroup);
	}
}