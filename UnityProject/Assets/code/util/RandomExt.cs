using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class RandomExt {
    public static T RandomElement<T>(this Transform enumerable)
    {
        return enumerable.Cast<Transform>()
            .Select(t => t.GetComponent<T>())
            .Where(t => t != null)
            .RandomElement();
    }
    public static T RandomElement<T>(this IEnumerable<T> enumerable)
    {
        var list = enumerable as IList<T> ?? enumerable.ToList();
        return list.ElementAt(Mathf.FloorToInt(Random.value * list.Count()));
    }
}
