
using Database.AisServcice;
using Database.MoodleService;
using Microsoft.Extensions.Configuration;

var moodleAPI = new MoodleAPI();
var aisAPI = new AisAPI();

var usersInAisDatabase = await aisAPI.GetUsersAsync();
var usersInMoodleDatabase = moodleAPI.GetUsers();




//var builder = new ConfigurationBuilder().Build();
//var pp = builder.GetSection("ConnectionStrings");

//IConfiguration config = new ConfigurationBuilder()
//	   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//	   .AddEnvironmentVariables()
//	   .Build();

//var configuration = new ConfigurationBuilder()

//	.SetBasePath(Directory.GetCurrentDirectory())
//	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//	.AddUserSecrets<Program>() // Если используете User Secrets
//	.AddEnvironmentVariables() // Если используете переменные окружения
//	.Build();

//var configuration = new ConfigurationBuilder().Add();

//var configuration = new ConfigurationBuilder()
//				.SetBasePath(Directory.GetCurrentDirectory())
//				.AddJsonFile("appsettings.json")
//				.Build();