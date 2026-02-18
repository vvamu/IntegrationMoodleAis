using Database.IntegrationAisMoodle.Application;
using Database.IntegrationAisMoodle.ViewModels;
using IntegrationAisMoodle.Application.Interfaces;
using IntegrationAisMoodle.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Integration.Controllers;


[ApiController]
[Route("ais/users")]
public partial class UserAisController(IUserService _userService) : ControllerBase
{

	[HttpGet]
	[Route("{id}/students")]
	public async Task<Object> GetUserByIdWithСlassmates(string id)
	{
		try
		{
			var result = await _userService.GetUserByNomzWithСlassmates(id);

			return result;
		}
		catch (Exception ex)
		{
			return new object();
		}
	}

	[HttpGet]
	[Route("{nomz}")]
	public async Task<Object> GetUsersAisHistory(string nomz)
	{
		try
		{
			var result = await _userService.GetAisUserHistoryAsync(nomz);

			return result;
		}
		catch (Exception ex)
		{
			return new object();
		}

	}
}
