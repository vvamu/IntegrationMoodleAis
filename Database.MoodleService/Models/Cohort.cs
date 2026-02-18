namespace Database.MoodleService.Models;

public class Cohort
{
	public int Id { get; set; }
	public string? Name { get; set; }

	public List<CohortMember> Members { get; set; }
}