namespace Database.MoodleService;

using Database.MoodleService.Models;
using Database.MoodleService.Repository;

public class MoodleAPI
{
	public async Task<List<User>> GetUsersAsync()
	{
		var connector = new UserRepository();
		var users = await connector.GetUsersAsync();
		return users;
	}

	public List<User> GetUsers()
	{
		var connector = new UserRepository();
		var users = connector.GetUsers();
		return users;
	}
}
