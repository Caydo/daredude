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

    protected int spawned = 0;

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



public class SpawnRateBasedOnSuccess : RateSpawnRules
{
    SpawnController spawner;

    SpawnController Spawner()
    {
        if(spawner == null)
        {
            spawner = Finder.Find<SpawnController>("GameController");
        }
        return spawner;
    }

    public override IEnumerable<Vehicle> SpawnVehicles(float timeElapsed)
    {
        var typeProbabilities = new List<ProbabilityTableEntry<string>>();
        float newRate = 0f;
        var successes = Spawner().successVehicles();
        if(successes <= 5)
        {
            newRate = 5;
            typeProbabilities.Add(new ProbabilityTableEntry<string>(1f, "Slow-Short"));
        } else if (successes <= 10)
        {
            newRate = 4;
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.5f, "Slow-Short"));
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.5f, "Fast-Short"));
        } else if (successes <= 15)
        {
            newRate = 4;
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.25f, "Slow-Short"));
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.75f, "Fast-Short"));
        } else if (successes <= 20)
        {
            newRate = 3;
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.25f, "Slow-Short"));
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.5f, "Fast-Short"));
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.25f, "Slow-Long")); 
        } else if (successes <= 25)
        {
            newRate = 3;
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.5f, "Fast-Short"));
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.25f, "Slow-Long"));
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.25f, "Fast-Long")); 
        } else 
        {
            newRate = 2;
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.25f, "Fast-Short"));
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.25f, "Slow-Long"));
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.5f, "Fast-Long")); 
        }

        if (rate != newRate)
        {
            rate = newRate;
            _type = typeProbabilities.rollFromTable();
            spawned = Mathf.FloorToInt(timeElapsed / rate); // don't want it to retroactively apply

            Debug.Log(string.Format("setting new spawn rate {0} {1} {2}", rate, _type, spawned));
        }

        return base.SpawnVehicles(timeElapsed);
    }
}