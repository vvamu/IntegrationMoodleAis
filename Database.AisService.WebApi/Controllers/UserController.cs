using Database.AisService.Models;
using Database.AisService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Database.AisService.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public UserController(IConfigurationSection config)
		{
			var connectionString = config.GetSection("ConnectionStrings").GetConnectionString("MsSQL");
		}

		[HttpGet]
        public async Task<List<User>> Get()
        {
			var userRepository = new UserRepository();
			var users = await userRepository.GetUsers();
			return users;
		}
    }
}
