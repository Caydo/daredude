using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic; 

public class SpawnController : MonoBehaviour {
    public Transform roads;
    public Transform successText;
    public Transform failText;
    public Vehicle carPrefab;

    public GameTransitionManager gameTransitionManager;

    public IList<string> spawnedVehicleTypes;
    public IList<string> successVehicleTypes;
    public IList<string> failVehicleTypes;

    public IList<EndCondition> endConditions;

    public int spawnedVehicles()
    {
        return spawnedVehicleTypes.Count();
    }
    public int successVehicles()
    {
        return successVehicleTypes.Count();
    }
    public int failVehicles()
    {
        return failVehicleTypes.Count();
    }

    void Start()
    {
        spawnedVehicleTypes = new List<string>();
        successVehicleTypes = new List<string>();
        failVehicleTypes = new List<string>();
        endConditions = new List<EndCondition>();

        // testing 
        //var endCondition1 = new SuccessfulVehiclesCondition();
        //endCondition1.successesNeeded = 20;
        var endCondition2 = new TooManyFailsCondition();
        endCondition2.failuresAllowed = 1;

        //endConditions.Add(endCondition1);
        endConditions.Add(endCondition2);
        // done testing
    }

    public void UpdateVehicleSpawned(string type, GameObject spawnedVehicle)
    {
        spawnedVehicleTypes.Add(type);
        gameTransitionManager.m_trafficCopVehicles.Add(spawnedVehicle);
    }

    public void UpdateVehicleSucceeded(string type)
    {
        successVehicleTypes.Add(type);
    }

    public void UpdateVehicleFailed(string type)
    {
        failVehicleTypes.Add(type);
    }

    void Update()
    {
        //if(Input.GetButtonUp("Fire3"))
        //{
        //    Transform randomRoad = roads.RandomElement<Transform>();
        //    var lane = randomRoad.Cast<Transform>().Where(t => !t.GetComponent<Lane>().allowEntry).First();
        //    Vehicle newVehicle = (Vehicle)Instantiate(carPrefab);
        //    newVehicle.transform.SetParent(lane, false);
        //    UpdateVehicleSpawned(newVehicle.type, newVehicle.gameObject);
        //}

        bool gotSuccess = endConditions.Select(cond => cond.checkSuccess(this)).Aggregate((soFar, next) => soFar || next);
        if(gotSuccess)
        {
            successText.gameObject.SetActive(true);
        }

        bool gotFail = endConditions.Select(cond => cond.checkFailure(this)).Aggregate((soFar, next) => soFar || next);
        if(gotFail)
        {
            failVehicleTypes.Clear();
            gameTransitionManager.StartStPeterPleaseGame();
        }

        if(gotSuccess || gotFail)
        {
            // TODO how to, like, pause the game, load level, etc?
        }
    }
}
