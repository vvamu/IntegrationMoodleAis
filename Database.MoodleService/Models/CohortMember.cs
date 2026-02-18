namespace Database.MoodleService.Models;

public class CohortMember
{
	public int Id { get; set; }
	public int CohortId { get; set; }
	public int UserId { get; set; }
}