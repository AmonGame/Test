using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class IListExtensions
{
	public static void Shuffle<T>(this IList<T> ts)
	{
		var count = ts.Count;
		var last = count - 1;
		for (var i = 0; i < last; ++i)
		{
			var r = UnityEngine.Random.Range(i, count);
			var tmp = ts[i];
			ts[i] = ts[r];
			ts[r] = tmp;
		}
	}

	public static void Shufless<T>(this IList<T> myList)
	{
		var shuffledList = myList.OrderBy(x => Random.value).ToList();
	}
}
