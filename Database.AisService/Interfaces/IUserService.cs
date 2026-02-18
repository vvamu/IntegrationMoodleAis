using Database.AisService.Models;
using Database.AisService.Repository;

namespace Database.AisService.Application;

internal interface IUserService
{
	public Task<List<User>> GetUserByNomzAsync(string nomz);
	public Task<List<User>> GetUsersAsync(int countRelativeMounth = 2);
	public Task<List<User>> GetUsersToCreateAsync(int countRelativeMounth = 2);
	public Task<List<User>> GetUsersToDeleteAsync(int countRelativeMounth = 2);
	public Task<List<User>> GetUsersToUpdateSurnameAsync(int countRelativeMounth = 2);
}