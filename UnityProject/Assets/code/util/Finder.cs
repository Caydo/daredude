using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Finder {
    // find all game objects with given tag and component (A)
    public static IEnumerable<A> FindAll<A>(string tag)
    {
        var foundObjs = GameObject.FindGameObjectsWithTag(tag);
        return foundObjs.Select(go => go.GetComponent<A>()).Where(x => x != null);
    }

    // find single game object with a given component (A)
    public static A Find<A>(string tag) where A : Object
    {
        var foundObj = GameObject.FindGameObjectWithTag(tag);

        if (foundObj == null)
        {
            return null; 
        }
        else
        {
            return foundObj.GetComponent<A>();
        }
    }

    // find all children of parent with this component
    public static IEnumerable<A> FindAll<A>(Transform parent)
    {
        return parent.Cast<Transform>().Select(c => c.GetComponent<A>()).Where(a => a != null).ToArray();
    }
}
