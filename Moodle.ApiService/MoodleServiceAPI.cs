using MoodleApiService.Helpers;
using MoodleApiService.Models;

namespace MoodleApiService;

public class MoodleServiceAPI
{
	public static async Task EditModule(int id, string action)
	{
		await UserService.EditModule(id, action);
	}

	public static async Task EditModules(List<UrlModule> urlModules, string action)
	{
		foreach (var urlModule in urlModules)
		{
			if (!urlModule.Visability) continue;

			await UserService.EditModule(urlModule.Id, action);

		}
	}


	public static async Task GetModule(int id)
	{
		await UserService.GetModule(id);
	}
}
