using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace JP.Utils
{
	public static class CollectionExtensions
	{
		/// <exception cref="ArgumentException">An element with the same key already exists.</exception>
		public static void Add<TKey, TValue>(this IDictionary<TKey, TValue> dict,
			IEnumerable<KeyValuePair<TKey, TValue>> other)
		{
			foreach (var entry in other)
				dict.Add(entry.Key, entry.Value);
		}
		
		public static void AddAndReplace<TKey, TValue>(this IDictionary<TKey, TValue> dict,
			IEnumerable<KeyValuePair<TKey, TValue>> other)
		{
			foreach (var entry in other)
				dict[entry.Key] = entry.Value;
		}

		public static int GetFastCount<T>(this IEnumerable<T> collection)
		{
			if (collection is IReadOnlyCollection<T> immutable)
				return immutable.Count;
			else if (collection is ICollection<T> mutable)
				return mutable.Count;
			else
				throw new ArgumentException($"{nameof(CollectionExtensions)}.{nameof(GetFastCount)} does not support type {collection.GetType().FullName}");
		}

		public static bool IsEqualContentTo<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dict,
			IEnumerable<KeyValuePair<TKey, TValue>> other)
			where TValue : IEquatable<TValue>
		{
			return dict.GetFastCount() == other.GetFastCount() &&
				dict.All(entry => other.TryGetValue(entry.Key, out var otherValue) &&
					entry.Value.Equals(otherValue));
		}

		public static bool TryGetValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dict,
			TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			if (dict is IReadOnlyDictionary<TKey, TValue> immutable)
				return immutable.TryGetValue(key, out value);
			else if (dict is IDictionary<TKey, TValue> mutable)
				return mutable.TryGetValue(key, out value);
			else
				throw new ArgumentException($"{nameof(CollectionExtensions)}.{nameof(TryGetValue)} does not support type {dict.GetType().FullName}");
		}
	}
}
