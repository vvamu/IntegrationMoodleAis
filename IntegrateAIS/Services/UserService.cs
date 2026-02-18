
using Database.AisServcice;
using Database.AisService.Models;
using Database.MoodleService;
using IntegrateAIS.Models;

namespace IntegrateAIS.Services;

public class UserService : IUserService
{
    private readonly MoodleAPI _moodleAPI;
    private readonly AisAPI _aisAPI;

    public UserService()
	{
		_moodleAPI = new MoodleAPI();
		_aisAPI = new AisAPI();
	}
	public async Task<List<UserViewModel>> CompareUsersByAisToCreate()
	{
		var usersInMoodleDatabase = await _moodleAPI.GetUsersNotUtilsAsync();
		var usersInAisDatabaseToCreate = await _aisAPI.GetUsersToCreateAsync(countRelativeMounth: 5);

		var moodleUserNames = new HashSet<string>(
			usersInMoodleDatabase.Select(u => u.UserName),
			StringComparer.OrdinalIgnoreCase); // или StringComparer.Ordinal если нужно точное совпадение

		var usersToCreate = usersInAisDatabaseToCreate
			.Where(aisUser => !moodleUserNames.Contains(aisUser.NomZ))
			.Select(aisUser => new UserViewModel
			{
				AisNomZ = aisUser.NomZ,
				AisSurname = aisUser.Surname,
				AisFirstName = aisUser.FirstName,
				AisMoveReason = aisUser.MoveReason,
				AisDatemoveserver = aisUser.Datemoveserver,
				MoodleId = 0, // Новый пользователь, еще нет в Moodle
				MoodleUserName = aisUser.NomZ,
				MoodleSurname = aisUser.Surname,
				MoodleFirstName = aisUser.FirstName
			})
			.ToList();

		return usersToCreate;
	}
	public async Task<List<UserViewModel>> CompareUsersByAisToDelete()
	{
		var usersInMoodleDatabase = await _moodleAPI.GetUsersNotUtilsAsync();
		var usersInAisDatabaseToDelete = await _aisAPI.GetUsersToDeleteAsync(countRelativeMounth: 5);

		var moodleUserNames = new HashSet<string>(
			usersInMoodleDatabase.Select(u => u.UserName),
			StringComparer.OrdinalIgnoreCase); 

		var usersToDelete = usersInAisDatabaseToDelete
			.Where(aisUser => moodleUserNames.Contains(aisUser.NomZ))
			.Select(aisUser => new UserViewModel
			{
				AisNomZ = aisUser.NomZ,
				AisSurname = aisUser.Surname,
				AisFirstName = aisUser.FirstName,
				AisMoveReason = aisUser.MoveReason,
				AisDatemoveserver = aisUser.Datemoveserver,
				MoodleId = 0, // Новый пользователь, еще нет в Moodle
				MoodleUserName = aisUser.NomZ,
				MoodleSurname = aisUser.Surname,
				MoodleFirstName = aisUser.FirstName
			})
			.ToList();

		return usersToDelete;
	}
	public async Task CompareUsersByAisToUpdateSurname()
	{
		var usersInMoodleDatabase = await _moodleAPI.GetUsersNotUtilsAsync();
		var usersInAisDatabaseToUpdate = await _aisAPI.GetUsersToUpdateSurnameAsync(countRelativeMounth: 5);

	}

}
