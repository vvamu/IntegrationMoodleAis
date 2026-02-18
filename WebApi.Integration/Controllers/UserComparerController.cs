using CsvHandler;
using CsvHandler.Models;
using Database.IntegrationAisMoodle.Application;
using Database.IntegrationAisMoodle.ViewModels;
using IntegrationAisMoodle.Application.Interfaces;
using IntegrationAisMoodle.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Integration.Controllers;

[ApiController]
[Route("comparer")]
public partial class UserComparerController(IUserService _userService) : ControllerBase
{

	[HttpGet]
	[Route("create")]
	public async Task<IEnumerable<UserViewModel>> CreateUsers(int countRelativeMounth = 5, int limit = 100)
	{
		try
		{
			if (limit <= 0) throw new NullReferenceException("UserController: Количество записей не может быть меньше нуля.");

			var result = await _userService.CreateCsvUsersToCreateAsync(countRelativeMounth);
			return result.Take(limit);
		}
		catch (Exception ex)
		{
			return new List<UserViewModel>();
		}

	}

	[HttpGet]
	[Route("check/create")]
	public async Task<IEnumerable<UserViewModel>> CompareUsersToCreate(int countRelativeMounth = 5, int limit = 100)
        {
		try
		{
			if (limit <= 0) throw new NullReferenceException("UserController: Количество записей не может быть меньше нуля.");
			var result = await _userService.CompareUsersByAisToCreateAsync(countRelativeMounth);
			
			return result.Take(limit);
		}
		catch (Exception ex) 
		{
			return new List<UserViewModel>();
		}
		
        }

	[HttpGet]
	[Route("check/delete")]
	public async Task<IEnumerable<UserViewModel>> GetUsersToDelete(int countRelativeMounth = 5,int limit = 100)
	{
		try
		{
			if (limit <= 0) throw new NullReferenceException("UserController: Количество записей не может быть меньше нуля.");
			var result = await _userService.CompareUsersByAisToDeleteAsync(countRelativeMounth);
			
			var notCorrect = result
				.Where(x => 
					(x.AisSurname?.ToLower().Trim().Replace(" ","") != x.MoodleSurname?.ToLower().Trim().Replace(" ", "")) 
					|| (x.AisFirstName?.ToLower().Trim().Replace(" ", "") != x.MoodleFirstName?.ToLower().Trim().Replace(" ", "")))
				.ToList();
			return result.Take(limit);
		}
		catch (Exception ex) 
		{
			return new List<UserViewModel>();
		}
	}

	[HttpGet]
	[Route("check/updateSurnames")]
	public async Task<IEnumerable<UserViewModel>> GetUsersToUpdateSurname(int countRelativeMounth = 5,int limit = 100)
	{
		try
		{
			if (limit <= 0) throw new NullReferenceException("UserController: Количество записей не может быть меньше нуля.");
			var result = await _userService.CompareUsersByAisToUpdateSurnameAsync(countRelativeMounth);
			var notCorrect = result
				.Where(x =>
				(x.AisSurname.ToLower().Trim().Replace(" ", "") != x.MoodleSurname.ToLower().Trim().Replace(" ", ""))
				|| (x.AisFirstName.ToLower().Trim().Replace(" ", "") != x.MoodleFirstName.ToLower().Trim().Replace(" ", ""))).ToList();
			return result.Take(limit);
		}
		catch (Exception ex)
		{
			return new List<UserViewModel>();
		}
	}
}
