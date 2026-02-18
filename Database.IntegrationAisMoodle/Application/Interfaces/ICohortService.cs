using Database.IntegrationAisMoodle.ViewModels;

namespace IntegrationAisMoodle.Application.Interfaces;

public interface ICohortService
{
	public Task<List<CohortMemberViewModel>> GetCohortMembersByCohortId(int cohortId);
	public Task<List<CohortViewModel>> GetCohortsByName(string partName);
}