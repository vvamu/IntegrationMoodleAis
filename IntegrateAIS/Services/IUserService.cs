using Database.AisServcice;
using Database.MoodleService;
using IntegrateAIS.Models;

namespace IntegrateAIS.Services;

public interface IUserService
{
	public Task<List<UserViewModel>> CompareUsersByAisToCreate(); // if exists in ais with status "to_create", but not in moodle
	public Task<List<UserViewModel>> CompareUsersByAisToDelete(); // if exists in ais with status "to_delete" and exists in moodle	
	public Task CompareUsersByAisToUpdateSurname(); // if exists in ais with status "update_surname" and exists in moodle but with old surname
}