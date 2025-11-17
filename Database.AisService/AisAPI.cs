namespace Database.AisServcice;

using Database.AisService.Models;
using Database.AisService.Repository;
using Microsoft.Data.SqlClient;

public class AisAPI
{
    public async Task<List<User>> GetUsersAsync()
    {
		var userRepository = new UserRepository();
		var users = await userRepository.GetUsers();
		return users;
	}


}
