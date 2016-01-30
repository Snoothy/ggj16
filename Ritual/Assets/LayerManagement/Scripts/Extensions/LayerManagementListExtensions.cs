using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class LayerManagementListExtensions {

	// Taken from
	// http://stackoverflow.com/questions/12231569/is-there-in-c-sharp-a-method-for-listt-like-resize-in-c-for-vectort
	public static void LayerManagementResize<T>(this List<T> list, int sz, T c = default(T))
	{
		int cur = list.Count;
		if(sz < cur)
			list.RemoveRange(sz, cur - sz);
		else if(sz > cur)
			list.AddRange(Enumerable.Repeat(c, sz - cur));
	}
}
