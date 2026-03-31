using System.Drawing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CsvHandler.Models;

public record CsvCreateElement
{
	public string Username { get; }
	public string Lastname { get; }
	public string Firstname { get; }
	public string Cohort { get; }

	public string Password => Username;
	public string Email => $"{Username}@dist.belstu.by";
	public string Course => "СДО БГТУ";
	public string Group => Cohort;

	public CsvCreateElement(string username, string lastname, string firstname, string cohort)
	{
		Username = username ?? throw new ArgumentNullException(nameof(username));
		Lastname = lastname ?? throw new ArgumentNullException(nameof(lastname));
		Firstname = firstname ?? throw new ArgumentNullException(nameof(firstname));
		Cohort = cohort ?? throw new ArgumentNullException(nameof(cohort));

		if (string.IsNullOrWhiteSpace(Username))
			throw new ArgumentException("Username cannot be empty", nameof(username));
		if (string.IsNullOrWhiteSpace(Lastname))
			throw new ArgumentException("Lastname cannot be empty", nameof(lastname));
		if (string.IsNullOrWhiteSpace(Firstname))
			throw new ArgumentException("Firstname cannot be empty", nameof(firstname));
		if (string.IsNullOrWhiteSpace(Cohort))
			throw new ArgumentException("Cohort cannot be empty", nameof(cohort));
	}

	public string ToCsvString(string separator = ",") =>
		string.Join(separator, Username, Password, Email, Lastname, Firstname, Course, Cohort, Group);

	public override string ToString() => ToCsvString();
}