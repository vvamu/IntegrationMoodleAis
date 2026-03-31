using CsvHandler.Models;
using System.Text;

namespace CsvHandler;

public static class СsvCreator
{
	public static async Task CreateAsync<T>(string fileName, IEnumerable<T> elements) where T : class
	{
		var mappedElements = elements.ToCsv();
		await WriteToCsvAsync(mappedElements, fileName);
	}

	public static async Task CreateAsync<T>(string fileName,IEnumerable<T> elements, Func<T, CsvCreateElement> mapper)
	{
		var mappedElements = elements.Select(mapper).ToList().ToCsv();
		await WriteToCsvAsync(mappedElements, fileName);	
	}

	//main
	private static async Task WriteToCsvAsync(string data, string filename)
	{
		var csv = new StringBuilder();
		var pathToFile = await GetFullFilePath(filename);

		csv.AppendLine(data);

		File.WriteAllText(pathToFile, csv.ToString());
	}

	private static async Task<string> GetFullFilePath(string origin_filename)
	{
		string workingDirectory = Environment.CurrentDirectory;
		var path = Directory.GetParent(workingDirectory).FullName;
		var fileName = origin_filename + "_" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour + "." + DateTime.Now.Minute + ".csv";
		string destPath = Path.Combine(path, "logs", fileName);
		return destPath;
	}
}
