using Database.IntegrationAisMoodle.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationAisMoodle.ViewModels.User;

public class UserViewModel
{
	
	public string? AisNomZ { get; set; } //Номер зачетки
	public string? AisSurname { get; set; } //Фамилия
	public string? AisFirstName { get; set; } //Имя и Отчество (И.О.)
	public string? AisGroup { get; set; }
	public string? AisGroupId { get; set; }
	public string? AisMoveReason { get; set; }
	public DateTime AisDateMoveServer { get; set; }

	public int MoodleId { get; set; }
	public string? MoodleUserName { get; set; } //Номер зачетки
	public string? MoodleSurname { get; set; } //Фамилия
	public string? MoodleFirstName { get; set; } //Имя и Отчество (И.О.)
	public string? MoodleGroup { get; set; } 

}