using Database.IntegrationAisMoodle.ViewModels;
using IntegrationAisMoodle.ViewModels.User;

namespace IntegrationAisMoodle.Application.Interfaces;

public interface IUserService
{
	public Task<List<UserViewModel>> GetMoodleUsersAsync();
	public Task<List<UserViewModel>> GetAisUsersAsync();
	public Task<Object> GetAisUserHistoryAsync(string nomz);

	public Task<Object> GetUserByNomzWithСlassmates(string nomZ);

	public Task<List<UserViewModel>> CreateCsvUsersToCreateAsync(int countRelativeMounth = 5);
	public Task<List<UserViewModel>> CompareUsersByAisToCreateAsync(int countRelativeMounth = 5); // if exists in ais with status "to_create", but not in moodle
	public Task<List<UserViewModel>> CompareUsersByAisToDeleteAsync(int countRelativeMounth = 5); // if exists in ais with status "to_delete" and exists in moodle	
	public Task<List<UserViewModel>> CompareUsersByAisToUpdateSurnameAsync(int countRelativeMounth = 5); // if exists in ais with status "update_surname" and exists in moodle but with old surname


}