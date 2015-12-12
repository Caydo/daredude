using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class SpawnRules
{
    protected abstract string type();
    public abstract IEnumerable<Vehicle> SpawnVehicles(float timeElapsed);

    Vehicle prefab = null;

    protected Vehicle spawn()
    {
        Vehicle retval = null; 

        if (prefab == null)
        {
            prefab = Resources.Load<Vehicle>(string.Format("prefabs/{0}", type()));
        }
        retval = (Vehicle) MonoBehaviour.Instantiate(prefab);
        return retval;
    }
}

public class FixTimeSpawn : SpawnRules
{
    public float fixedTime; // number of seconds for when this spawn rule will fire 
    public string _type; 
    bool hasSpawned;

    override protected string type()
    {
        return _type;
    }

    override public IEnumerable<Vehicle> SpawnVehicles(float timeElapsed)
    {
        IList<Vehicle> retval = new List<Vehicle>();
        if (timeElapsed > fixedTime && !hasSpawned)
        {
            retval.Add(spawn());
            hasSpawned = true; 
        }
        return retval;
    }
}

public class RateSpawnRules : SpawnRules
{
    public float startAt; // start implementing this rule at a fixed time 
    public float rate; // secs between cars
    public string _type; 

    int spawned = 0;

    override protected string type()
    {
        return _type;
    }

    override public IEnumerable<Vehicle> SpawnVehicles(float timeElapsed)
    {
        IList<Vehicle> retval = new List<Vehicle>();

        var toSpawn = Mathf.FloorToInt((timeElapsed - startAt) / rate) - spawned;
        if (toSpawn > 0)
        {
            spawned += toSpawn;
            for(int i = 0; i < toSpawn; ++i)
            {
                retval.Add(spawn());
            }
        }

        return retval;
    }

}
