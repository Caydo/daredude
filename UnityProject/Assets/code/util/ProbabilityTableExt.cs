using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        var sortedList = table.OrderBy(p => p.probability);
        var retval = sortedList.First();
        float roll = Random.Range(0f, 1f);
        var rangeUsed = 0f;
        foreach(var tableEntry in sortedList.Skip(1))
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
