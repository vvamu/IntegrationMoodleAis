namespace Database.AisService.Models;

public class User
{
	public string? NomZ { get; set; } //Номер зачетки
	public string? LastName { get; set; } //Фамилия
	public string? MoveReason { get; set; } 
	public DateTime Datemove { get; set; }
	public DateTime Datemoveserver { get; set; }

}
