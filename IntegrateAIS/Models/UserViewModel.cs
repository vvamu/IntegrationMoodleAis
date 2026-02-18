using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrateAIS.Models;

public class UserViewModel
{
	
	public string? AisNomZ { get; set; } //Номер зачетки
	public string? AisSurname { get; set; } //Фамилия
	public string? AisFirstName { get; set; } //Имя и Отчество (И.О.)
	public string? AisMoveReason { get; set; }
	public DateTime AisDatemoveserver { get; set; }

	public int MoodleId { get; set; }
	public string? MoodleUserName { get; set; } //Номер зачетки
	public string? MoodleSurname { get; set; } //Имя и Отчество (И.О.)
	public string? MoodleFirstName { get; set; } //Имя и Отчество (И.О.)

}