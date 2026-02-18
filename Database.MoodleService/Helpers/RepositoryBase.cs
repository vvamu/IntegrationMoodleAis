namespace Database.MoodleService.Helpers;

using Database.MoodleService.Models;



internal class RepositoryBase
{
	protected static int ParseInt(object value) =>
	Convert.ToInt32(value ?? throw new ArgumentNullException(nameof(value)));

	protected static string ParseString(object value) =>
		Convert.ToString(value) ?? string.Empty;

	protected static (object id, object username, object lastname, object firstname, object groups) ExtractTupleValuesToUser(object tuple)
	{
		return tuple switch
		{
			Tuple<object, object, object, object, object> t => (t.Item1, t.Item2, t.Item3, t.Item4, t.Item5),
			ValueTuple<object, object, object, object, object> vt => (vt.Item1, vt.Item2, vt.Item3, vt.Item4, vt.Item5),
			_ => throw new InvalidOperationException($"Unsupported tuple type: {tuple.GetType()}")
		};
	}

	protected static (object id, object name) ExtractTupleValuesToCohort(object tuple)
	{
		return tuple switch
		{
			Tuple<object, object> t => (t.Item1, t.Item2),
			ValueTuple<object, object> vt => (vt.Item1, vt.Item2),
			_ => throw new InvalidOperationException($"Unsupported tuple type: {tuple.GetType()}")
		};
	}

	protected static (object id, object cohortId, object userId) ExtractTupleValuesToCohortMembers(object tuple)
	{
		return tuple switch
		{
			Tuple<object, object, object> t => (t.Item1, t.Item2, t.Item3),
			ValueTuple<object, object, object> vt => (vt.Item1, vt.Item2, vt.Item3),
			_ => throw new InvalidOperationException($"Unsupported tuple type: {tuple.GetType()}")
		};
	}

}