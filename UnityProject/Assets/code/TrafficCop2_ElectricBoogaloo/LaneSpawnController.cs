using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaneSpawnController : MonoBehaviour {
    public IList<SpawnRules> spawnRules;

    public SpawnController globalSpawnController;

    float timeElapsed = 0;

	void Start () {
        globalSpawnController = Finder.Find<SpawnController>("GameController");
        spawnRules = new List<SpawnRules>();
        spawnRules.Add(new SpawnRateBasedOnSuccess());

        // testing
        //int positionInParent = 0; 
        //for(positionInParent = 0; positionInParent < transform.parent.parent.childCount; ++positionInParent)
        //{
        //    Transform curChild = transform.parent.parent.GetChild(positionInParent); 
        //    if(curChild == transform.parent)
        //    {
        //        break;
        //    }
        //}

        ////Debug.Log(string.Format("found position in parent {0}", positionInParent));
        //var rateSpawnRules = new RateSpawnRules();
        //rateSpawnRules._type = "Car";
        //rateSpawnRules.rate = 5; // 5 sec / car 
        //rateSpawnRules.startAt = positionInParent;
        //spawnRules.Add(rateSpawnRules);

        //var secondSpawnRule = new FixTimeSpawn();
        //secondSpawnRule._type = "Car";
        //secondSpawnRule.fixedTime = 2f;
        //spawnRules.Add(secondSpawnRule);
        // end testing
	}

    void Update()
    {
        timeElapsed += Time.deltaTime;
        foreach(var rule in spawnRules)
        {
            var newSpawns = rule.SpawnVehicles(timeElapsed); 
            foreach(Vehicle v in newSpawns)
            {
                v.transform.SetParent(transform, false);
                globalSpawnController.UpdateVehicleSpawned(v.type);
            }
        }
    }
}
