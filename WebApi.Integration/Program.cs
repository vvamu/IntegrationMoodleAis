using Database.IntegrationAisMoodle.Application;
using IntegrationAisMoodle.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICohortService, CohortService>();
builder.Services.AddTransient<IGroupService, GroupService>();

var app = builder.Build();


app.UseSwagger(); // Enable Swagger middleware
app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
	c.RoutePrefix = string.Empty; // Делаем Swagger UI доступным по корню
});


app.UsePathBase("/swagger");
app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("/", context =>
{
	context.Response.Redirect("/swagger");
	return Task.CompletedTask;
});

app.Run();
