using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class SpawnRules
{
    protected abstract string type();
    public abstract IEnumerable<Vehicle> SpawnVehicles(float timeElapsed);

    protected Vehicle prefab = null;

    virtual protected Vehicle spawn()
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

    IList<ProbabilityTableEntry<string>> currentTypeProbabilities;
    int currentEntry = 0; // completely arbitrary current entry id

    protected override string type()
    {
        return currentTypeProbabilities.rollFromTable();
    }

    override protected Vehicle spawn()
    {
        if(Random.Range(0f,1f) < .25f)
        {
            return base.spawn();
        } 
        else
        {
            return null;
        }
    }

    public override IEnumerable<Vehicle> SpawnVehicles(float timeElapsed)
    {
        var typeProbabilities = new List<ProbabilityTableEntry<string>>();
        float newRate = 0f;
        var successes = Spawner().successVehicles();
        var newEntry = 0;
        if(successes <= 5)
        {
            newRate = 1.25f;
            typeProbabilities.Add(new ProbabilityTableEntry<string>(1f, "Slow-Short"));
            newEntry = 1;
        } else if (successes <= 10)
        {
            newRate = 1;
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.5f, "Slow-Short"));
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.5f, "Fast-Short"));
            newEntry = 2;
        } else if (successes <= 15)
        {
            newRate = 1;
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.25f, "Slow-Short"));
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.75f, "Fast-Short"));
            newEntry = 3;
        } else if (successes <= 20)
        {
            newRate = .75f;
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.25f, "Slow-Short"));
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.5f, "Fast-Short"));
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.25f, "Slow-Long"));
            newEntry = 4;
        } else if (successes <= 25)
        {
            newRate = .75f;
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.5f, "Fast-Short"));
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.25f, "Slow-Long"));
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.25f, "Fast-Long"));
            newEntry = 5;
        } else 
        {
            newRate = .5f;
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.25f, "Fast-Short"));
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.25f, "Slow-Long"));
            typeProbabilities.Add(new ProbabilityTableEntry<string>(.5f, "Fast-Long"));
            newEntry = 6;
        }

        if (currentEntry != newEntry)
        {
            rate = newRate;
            currentTypeProbabilities = typeProbabilities;
            spawned = Mathf.FloorToInt(timeElapsed / rate); // don't want it to retroactively apply
            prefab = null;
            currentEntry = newEntry;

            //Debug.Log(string.Format("setting new spawn rate {0} {1} {2}", rate, _type, spawned));
        }

        return base.SpawnVehicles(timeElapsed);
    }
}