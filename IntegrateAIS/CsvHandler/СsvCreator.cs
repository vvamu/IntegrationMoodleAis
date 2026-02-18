using CsvHandler.Models;
using System.Text;

namespace CsvHandler;

public static class СsvCreator
{
	public static async Task CreateAsync<T>(IEnumerable<T> elements,
										  Func<T, CsvCreateElement> mapper)
	{
		var mappedElements = elements.Select(mapper).ToList();
		await WriteToCsvAsync(mappedElements);
	}
	private static async Task WriteToCsvAsync(List<CsvCreateElement> elements)
	{
		var csv = new StringBuilder();
		string workingDirectory = Environment.CurrentDirectory;
		var path = Directory.GetParent(workingDirectory).Parent.FullName;
		var fileName = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour + "." + DateTime.Now.Minute + ".csv";
		path = Directory.GetParent(workingDirectory).FullName;
		path = path;
		string destPath = Path.Combine(path, "logs", fileName);


		csv.AppendLine("username,password,email,lastname,firstname,course1,cohort1,group1");

		foreach (var el in elements)
		{
			csv.AppendLine(el.ToString());
		}
		
		File.WriteAllText(destPath, csv.ToString());
	}

}
