using Database.IntegrationAisMoodle.Application;
using Database.IntegrationAisMoodle.ViewModels;
using Database.MoodleService.Models;
using IntegrationAisMoodle.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Integration.Controllers;

[ApiController]
[Route("ais/groups")]
public class GroupAisController(IGroupService _groupService, IUserService _userService) : ControllerBase
{
	[HttpGet]
	[Route("{id}/students")]
	public async Task<Object> GetUsersByGroupId(int groupId)
	{
		try
		{
			var result = await _groupService.GetUsersByGroupId(groupId);

			return result;
		}
		catch (Exception ex)
		{
			return new object();
		}
	}
}

