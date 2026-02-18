using Database.IntegrationAisMoodle.Application;
using Database.IntegrationAisMoodle.ViewModels;
using IntegrationAisMoodle.Application.Interfaces;
using IntegrationAisMoodle.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Integration.Controllers;


[ApiController]
[Route("moodle/users")]
public partial class UserMoodleController(ICohortService _cohortService) : ControllerBase
{
	[HttpGet]
	[Route("{id}/groups")]
	public async Task<List<CohortViewModel>> GetCohortsByName(string partName)
	{
		try
		{
			var result = await _cohortService.GetCohortsByName(partName);

			return result;
		}
		catch (Exception ex)
		{
			return new List<CohortViewModel>();
		}
	}
}
