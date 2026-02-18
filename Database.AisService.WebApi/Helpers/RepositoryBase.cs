using Database.AisService.Models;

namespace Database.AisService.Repository;

internal class RepositoryBase 
{
	protected static int ParseInt(object value) =>
	Convert.ToInt32(value ?? throw new ArgumentNullException(nameof(value)));

	protected static string ParseString(object value) =>
		Convert.ToString(value) ?? string.Empty;

	protected static DateTime ParseDate(object value) =>
		Convert.ToDateTime(value);

	protected static (object id, object username, object lastname, object firstname) ExtractTupleValues(object tuple)
	{
		return tuple switch
		{
			Tuple<object, object, object, object> t => (t.Item1, t.Item2, t.Item3, t.Item4),
			ValueTuple<object, object, object, object> vt => (vt.Item1, vt.Item2, vt.Item3, vt.Item4),
			_ => throw new InvalidOperationException($"Unsupported tuple type: {tuple.GetType()}")
		};
	}

}