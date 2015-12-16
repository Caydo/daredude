using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;

public class ProbabilityTableEntry<A>
{
    public float probability;
    public A thingy;
    public ProbabilityTableEntry(float probability, A thingy)
    {
        this.probability = probability;
        this.thingy = thingy;
    }
}

public static class ProbabilityTableExt {
    public static A rollFromTable<A>(this IEnumerable<ProbabilityTableEntry<A>> table)
    {
        List<ProbabilityTableEntry<A>> sortedList = new List<ProbabilityTableEntry<A>>(table); //.OrderBy(p => p.probability);
        sortedList.Sort((a, b) =>
        {
            return a.probability.CompareTo(b.probability);
        });

        var retval = sortedList[0];
        float roll = Random.Range(0f, 1f);
        var rangeUsed = 0f;
        foreach (var tableEntry in sortedList.GetRange(1, sortedList.Count - 1))
        {
            if(roll < (rangeUsed + tableEntry.probability))
            {
                return tableEntry.thingy;
            }
            rangeUsed += tableEntry.probability;
        }

        return retval.thingy;
    }
}

