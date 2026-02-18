using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Database.AisService.Models;

public class User
{
	public int? Id { get; set; }
	public string? NomZ { get; set; } //Номер зачетки
	public string? Surname { get; set; } //Фамилия
	public string? ShortName { get; set; } //Имя и Отчество (И.О.)
	public string? Group { get; set; }

	public string? Subgroup { get; set; }
	public string? IsFzo { get; set; }
	public string? Speciality { get; set; }
	public string? Specialization { get; set; }




	public string? GroupId { get; set; }
	public string? MoveReason { get; set; } 
	public DateTime Datemove { get; set; }
	public DateTime Datemoveserver { get; set; }

}


