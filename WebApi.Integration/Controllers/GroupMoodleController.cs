using Database.IntegrationAisMoodle.ViewModels;
using Database.MoodleService.Models;
using IntegrationAisMoodle.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Integration.Controllers;

[ApiController]
[Route("moodle/group")]
public class GroupMoodleController(ICohortService _cohortService) : ControllerBase
{
	[HttpGet]
	[Route("{id}/members")]
	public async Task<List<CohortMemberViewModel>> GetCohortMembersByCohortId(int id)
	{
		try
		{
			var result = await _cohortService.GetCohortMembersByCohortId(id);

			return result;
		}
		catch (Exception ex)
		{
			return new List<CohortMemberViewModel>();
		}
	}

	
}

