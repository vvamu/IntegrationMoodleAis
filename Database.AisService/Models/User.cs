using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Database.AisService.Models;
public record class User
{
	public int? Id { get; init; }
	public string? NomZ { get; init; } //Номер зачетки
	public string? Surname { get; init; } //Фамилия
	public string? ShortName { get; init; } //Имя и Отчество (И.О.)
	public string? Group { get; init; }
	public string? Subgroup { get; init; }
	public string? IsFzo { get; init; }
	public string? Speciality { get; init; }
	public string? Specialization { get; init; }
	public string? GroupId { get; init; }
	public string? MoveReason { get; init; }
	public DateTime Datemove { get; init; }
	public DateTime Datemoveserver { get; init; }
}