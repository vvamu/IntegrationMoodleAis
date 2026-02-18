namespace Database.AisServcice;

using Database.AisService.Application;
using Database.AisService.Models;
using Database.AisService.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class AisAPI : IUserService
{
	private readonly UserRepository _userRepository;
	private readonly GroupRepository _groupRepository;
	public AisAPI(IConfigurationSection config = null)
	{
		var connectionString = config?.GetSection("ConnectionStrings").GetConnectionString("MsSQL");
		_userRepository = new UserRepository();
		_groupRepository = new GroupRepository();
	}
	#region User

	#endregion

	public async Task<List<User>> GetUserByNomzAsync(string nomz)
	{
		var countMounth = 12 * 10; //10 лет
		var users = await _userRepository.GetLastUserActions(countMounth, nomz);
		return users;
	}

	public async Task<List<User>> GetUsersAsync(int countRelativeMounth = 2)
	{
		var users = await _userRepository.GetLastUsersActions(countRelativeMounth);
		return users;
	}
	public async Task<List<User>> GetUsersToCreateAsync(int countRelativeMounth = 2)
	{
		var users = await _userRepository.GetLastUsersActions(countRelativeMounth: countRelativeMounth, toCreate: true);
		return users;
	}

	public async Task<List<User>> GetUsersToDeleteAsync(int countRelativeMounth = 2)
	{
		var users = await _userRepository.GetLastUsersActions(countRelativeMounth: countRelativeMounth, toDelete: true);
		return users;
	}

	public async Task<List<User>> GetUsersToUpdateSurnameAsync(int countRelativeMounth = 2)
	{
		var users = await _userRepository.GetLastUsersActions(countRelativeMounth: countRelativeMounth, toChangeSurname: true);
		return users;
	}


	#region Group

	public async Task<Object> GetUserByNomzWithСlassmates(string nomZ)
	{
		var users = await _groupRepository.GetUserByNomzWithСlassmates(nomZ);
		return users;
	}
	public async Task<Object> GetUsersByGroupId(int groupId)
	{
		var users = await _groupRepository.GetUsersByGroupId(groupId);
		return users;
	}

	#endregion

}
