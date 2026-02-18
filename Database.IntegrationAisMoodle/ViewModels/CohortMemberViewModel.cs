using Database.MoodleService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.IntegrationAisMoodle.ViewModels;

public class CohortMemberViewModel
{
	public int Id { get; set; }
	public int? CohortId { get; set; }
	public string? CohortName { get; set; }
	public int? UserId { get; set; }

}