
using Database.AisServcice;
using Database.AisService.Models;
using Database.IntegrationAisMoodle.ViewModels;
using Database.MoodleService;
using IntegrationAisMoodle.Application.Interfaces;
using System.Text.RegularExpressions;

namespace Database.IntegrationAisMoodle.Application;

public class GroupService : IGroupService
{
    private readonly AisAPI _aisAPI;

    public GroupService()
	{
		_aisAPI = new AisAPI();

	}
	

	public Task<object> GetUsersByGroupId(int groupId)
	{
		var users = _aisAPI.GetUsersByGroupId(groupId);
		return users;
	}
}
