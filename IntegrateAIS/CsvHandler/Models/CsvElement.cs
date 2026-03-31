using System.Drawing;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CsvHandler.Models;

public static class CsvExtensions
{
	public static string ToCsvRow<T>(this T obj, string separator = ",") where T : class
	{
		if (obj == null) return string.Empty;

		var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
		var values = properties.Select(p => p.GetValue(obj)?.ToString() ?? "");

		return string.Join(separator, values.Select(EscapeCsvValue));
	}

	public static string GetCsvHeader<T>(string separator = ",") where T : class
	{
		var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
		return string.Join(separator, properties.Select(p => p.Name));
	}

	private static string EscapeCsvValue(string value)
	{
		if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
		{
			return $"\"{value.Replace("\"", "\"\"")}\"";
		}
		return value;
	}

	// Для коллекций
	public static string ToCsv<T>(this IEnumerable<T> items, string separator = ",") where T : class
	{
		if (!items.Any()) return string.Empty;

		var header = GetCsvHeader<T>(separator);
		var rows = items.Select(item => item.ToCsvRow(separator));

		return string.Join(Environment.NewLine, new[] { header }.Concat(rows));
	}
}