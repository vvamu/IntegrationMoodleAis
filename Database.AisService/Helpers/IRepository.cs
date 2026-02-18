using Database.AisService.Models;
using System.Runtime.CompilerServices;

namespace Database.AisService.Helpers;

internal class Repository 
{
	protected static int ParseInt(object value) =>
	Convert.ToInt32(value ?? throw new ArgumentNullException(nameof(value)));

	protected static string ParseString(object value) =>
		Convert.ToString(value) ?? string.Empty;

	protected static DateTime ParseDate(object value) =>
		Convert.ToDateTime(value);

	//protected static (object id, object nomz, object surname, object lastname, object group, object groupId, object movereason, object datemove) ExtractTupleValues(object tuple)
	//{
	//	return tuple switch
	//	{
	//		Tuple<object, object, object, object, object, object, object, object> t => (t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, t.Item6, t.Item7, t.Item8),
	//		ValueTuple<object, object, object, object, object, object, object, object> vt => (vt.Item1, vt.Item2, vt.Item3, vt.Item4, vt.Item5, vt.Item6, vt.Item7, vt.Item8),
	//		_ => throw new InvalidOperationException($"Unsupported tuple type: {tuple.GetType()}")
	//	};
	//}

	protected static (T1, T2, T3, T4, T5, T6, T7, T8?) ExtractTupleValues<T1, T2, T3, T4, T5, T6, T7, T8>(object tuple)
	{
		if (tuple is ITuple ituple)
		{
			return (
				ituple.Length > 0 ? (T1)ituple[0] : default!,
				ituple.Length > 1 ? (T2)ituple[1] : default!,
				ituple.Length > 2 ? (T3)ituple[2] : default!,
				ituple.Length > 3 ? (T4)ituple[3] : default!,
				ituple.Length > 4 ? (T5)ituple[4] : default!,
				ituple.Length > 5 ? (T6)ituple[5] : default!,
				ituple.Length > 6 ? (T7)ituple[6] : default!,
				ituple.Length > 7 ? (T8)ituple[7] : default
			);
		}

		throw new InvalidOperationException($"Unsupported tuple type: {tuple.GetType()}");
	}



	protected static Dictionary<string, object?> ExtractNamedTupleValues(object tuple)
	{
		var result = new Dictionary<string, object?>();
		var type = tuple.GetType();

		if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ValueTuple<,>).Assembly.GetType("System.ValueTuple"))
		{
			var fields = type.GetFields();
			foreach (var field in fields)
			{
				// Пытаемся получить имена полей из атрибутов
				var name = field.Name;
				result[name] = field.GetValue(tuple);
			}
		}
		else if (tuple is ITuple ituple)
		{
			for (int i = 0; i < ituple.Length; i++)
			{
				result[$"Item{i + 1}"] = ituple[i];
			}
		}

		return result;
	}

}