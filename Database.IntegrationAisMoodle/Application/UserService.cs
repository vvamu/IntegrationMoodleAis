
using CsvHandler;
using Database.AisServcice;
using Database.AisService.Models;
using Database.MoodleService;
using IntegrationAisMoodle.Application.Interfaces;
using IntegrationAisMoodle.ViewModels.User;

namespace Database.IntegrationAisMoodle.Application;

public class UserService : IUserService
{
    private readonly MoodleAPI _moodleAPI;
    private readonly AisAPI _aisAPI;

    public UserService()
	{
		_moodleAPI = new MoodleAPI();
		_aisAPI = new AisAPI();
	}

	#region Ais

	public Task<object> GetUserByNomzWithСlassmates(string nomZ)
	{
		var users = _aisAPI.GetUserByNomzWithСlassmates(nomZ);
		return users;
	}
	public async Task<object> GetAisUserHistoryAsync(string nomz)
	{
		var usersInAisDatabase = await _aisAPI.GetUserByNomzAsync(nomz);

		var users = usersInAisDatabase
			.Select(aisUser =>
			{
				return UserMappingProfile.ToViewModel(aisUser);
			})
			.ToList();
		return users;
	}
	public async Task<List<UserViewModel>> GetAisUsersAsync()
	{
		var usersInAisDatabase = await _aisAPI.GetUsersAsync(countRelativeMounth: 5);

		var users = usersInAisDatabase
			.Select(aisUser =>
			{
				return UserMappingProfile.ToViewModel(aisUser);
			})
			.ToList();
		return users;
	}

	#endregion

	#region Moodle

	public async Task<List<UserViewModel>> GetMoodleUsersAsync()
	{
		var usersInMoodleDatabase = await _moodleAPI.GetUsersNotUtilsAsync();
		var moodleUsersDict = usersInMoodleDatabase
		.ToDictionary(u => u.UserName, u => u, StringComparer.OrdinalIgnoreCase);

		var res = usersInMoodleDatabase.Select(moodleUser =>
		{
			return new UserViewModel
			{
				
				MoodleId = moodleUser.Id,
				MoodleUserName = moodleUser.UserName,
				MoodleSurname = moodleUser.Surname ,
				MoodleFirstName = moodleUser.FirstName
			};
		})
		.ToList();

		return res;
	}

	#endregion

	public async Task<List<UserViewModel>> CompareUsersByAisToCreateAsync(int countRelativeMounth = 5)
	{
		var usersInMoodleDatabase = await _moodleAPI.GetUsersNotUtilsAsync();
		var usersInAisDatabaseToCreate = await _aisAPI.GetUsersToCreateAsync(countRelativeMounth);

		var moodleUsersDict = usersInMoodleDatabase
			.ToDictionary(u => u.UserName, u => u, StringComparer.OrdinalIgnoreCase);

		var usersToCreate = usersInAisDatabaseToCreate
			.Select(aisUser =>
			{
				var existsInMoodle = moodleUsersDict.TryGetValue(aisUser.NomZ, out var moodleUser);
				return UserMappingProfile.ToViewModel(aisUser, moodleUser);
			})
			.ToList();


		return usersToCreate;
	}

	public async Task<List<UserViewModel>> CreateCsvUsersToCreateAsync(int countRelativeMounth = 5)
	{
		var usersInMoodleDatabase = await _moodleAPI.GetUsersNotUtilsAsync();
		var usersInAisDatabaseToCreate = await _aisAPI.GetUsersToCreateAsync(countRelativeMounth);

		var moodleUsersDict = usersInMoodleDatabase
			.ToDictionary(u => u.UserName, u => u, StringComparer.OrdinalIgnoreCase);

		var usersToCreate = usersInAisDatabaseToCreate
			.Where(aisUser => !moodleUsersDict.ContainsKey(aisUser.NomZ))
			.Select(aisUser => UserMappingProfile.ToViewModel(aisUser, null))
			.ToList();


		await СsvCreator.CreateAsync(usersToCreate, user => UserMappingProfile.ToCreateCsvViewModel(user));

		return usersToCreate;
	}
	public async Task<List<UserViewModel>> CompareUsersByAisToDeleteAsync(int countRelativeMounth = 5)
	{
		var usersInMoodleDatabase = await _moodleAPI.GetUsersNotUtilsAsync();
		var usersInAisDatabaseToDelete = await _aisAPI.GetUsersToDeleteAsync(countRelativeMounth);

		var moodleUsersDict = usersInMoodleDatabase
		.ToDictionary(u => u.UserName, u => u, StringComparer.OrdinalIgnoreCase);

		var usersToDelete = usersInAisDatabaseToDelete
			.Select(aisUser =>
			{
				var existsInMoodle = moodleUsersDict.TryGetValue(aisUser.NomZ, out var moodleUser);
				if (existsInMoodle == null) return null;
				return UserMappingProfile.ToViewModel(aisUser,moodleUser);
			})
			.ToList();

		return usersToDelete;
	}
	public async Task<List<UserViewModel>> CompareUsersByAisToUpdateSurnameAsync(int countRelativeMounth = 5)
	{
		var usersInMoodleDatabase = await _moodleAPI.GetUsersNotUtilsAsync();
		var usersInAisDatabaseToUpdate = await _aisAPI.GetUsersToUpdateSurnameAsync(countRelativeMounth);

		var moodleUsersDict = usersInMoodleDatabase
		.ToDictionary(u => u.UserName, u => u, StringComparer.OrdinalIgnoreCase);

		var usersToUpdate = usersInAisDatabaseToUpdate
			.Select(aisUser =>
			{
				var existsInMoodle = moodleUsersDict.TryGetValue(aisUser.NomZ, out var moodleUser);
				if (existsInMoodle == null) return null;

				return UserMappingProfile.ToViewModel(aisUser, moodleUser);
			})
			.ToList();
		return usersToUpdate;
	}

}
