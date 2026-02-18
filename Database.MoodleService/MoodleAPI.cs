namespace Database.MoodleService;
using Database.MoodleService.Models;
using Database.MoodleService.Repository;
using Microsoft.Extensions.Configuration;
using static Org.BouncyCastle.Math.EC.ECCurve;

public class MoodleAPI
{
	public MoodleAPI(IConfigurationSection config = null)
	{
		var connectionString = config?.GetSection("ConnectionStrings").GetConnectionString("MySql");
		var localConfig = config?.GetSection("LocalConfiguration");
		var proxyConfig = config?.GetSection("ProxyConfiguration");
		var sshConfig = config?.GetSection("SshConfiguration");

	}

	public async Task<List<User>> GetUsersAsync()
	{
		var userRepository = new UserRepository();
		var users = await userRepository.GetUsersAsync();
		return users;
	}

	public async Task<List<User>> GetUsersNotUtilsAsync()
	{

		string sqlExpressionWithConditions = "";

		var userRepository = new UserRepository();
		var users = await userRepository.GetUsersAsync();

		return users;
	}


	public async Task<List<Cohort>> GetCohortsByNameAsync(string cohortNamePart)
	{
		var cohortRep = new CohortRepository();
		var users = await cohortRep.GetCohortsByNameAsync(cohortNamePart);
		return users;
	}

	public async Task<List<CohortMember>> GetCohortMembersByCohortId(int cohortId)
	{

		string sqlExpressionWithConditions = "";

		var cohortRep = new CohortMemberRepository();
		var users = await cohortRep.GetUsersByCohortId(cohortId);

		return users;
	}


}
