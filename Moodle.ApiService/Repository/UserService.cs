using System;
using System.Collections.Generic;
using System.Text;

namespace MoodleApiService.Helpers;

internal class UserService : BaseMoodleService
{
	public static async Task EditModule(int id, string action)
	{
		var function = "wsfunction=" + "core_course_edit_module";
		var pId = "id=" + id;
		var pAction = "action=" + action;

		var parameters = function + "&" + pId + "&" + pAction;
		var request = baseRequest + "&" + parameters;

		var res = await _httpClient.GetAsync(request);

	}

	public static async Task GetModule(int id)
	{
		

		var function = "wsfunction=" + "core_course_get_module";
		var pId = "id=" + id.ToString();

		var parameters = function + "&" + id;

		var request = baseRequest + "&" + parameters;

		var res = await _httpClient.GetAsync(request);

	}
}
