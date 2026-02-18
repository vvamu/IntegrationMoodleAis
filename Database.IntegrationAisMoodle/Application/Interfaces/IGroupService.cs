using Database.IntegrationAisMoodle.ViewModels;

namespace IntegrationAisMoodle.Application.Interfaces;

public interface IGroupService
{
	public Task<Object> GetUsersByGroupId(int groupId);
	
}