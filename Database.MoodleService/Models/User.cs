namespace Database.MoodleService.Models;

public class User
{
	public int Id { get; set; }
	public string? UserName { get; set; } //Номер зачетки
	public string? LastName { get; set; } //Фамилия
	public string? FirstName { get; set; } //Имя и Отчество (И.О.)

}
