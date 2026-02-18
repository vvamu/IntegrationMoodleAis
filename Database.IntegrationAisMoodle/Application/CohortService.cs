
using Database.AisServcice;
using Database.AisService.Models;
using Database.IntegrationAisMoodle.ViewModels;
using Database.MoodleService;
using IntegrationAisMoodle.Application.Interfaces;

namespace Database.IntegrationAisMoodle.Application;

public class CohortService : ICohortService
{
    private readonly MoodleAPI _moodleAPI;

    public CohortService()
	{
		_moodleAPI = new MoodleAPI();

	}

	public async Task<List<CohortViewModel>> GetCohortsByName(string partName)
	{
		var cohorts = await  _moodleAPI.GetCohortsByNameAsync(partName);
		var cohortsViewModels = cohorts.Select(cohort =>
		{
			return new CohortViewModel
			{
				Id = cohort.Id,
				Name = cohort.Name,
			};
		})
		.ToList();
		return cohortsViewModels;


	}

	public async Task<List<CohortMemberViewModel>> GetCohortMembersByCohortId(int cohortId)
	{
		var cohortMembers = await _moodleAPI.GetCohortMembersByCohortId(cohortId);
		var cohortsViewModels = cohortMembers.Select(cohort_m =>
		{
			return new CohortMemberViewModel
			{
				Id = cohort_m.Id,
				CohortId = cohort_m.CohortId,
				UserId = cohort_m.UserId,

			};
		})
		.ToList();
		return cohortsViewModels;
	}

}
