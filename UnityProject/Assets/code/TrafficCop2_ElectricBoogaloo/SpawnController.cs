using UnityEngine;
using System.Collections;
using System.Linq; 

public class SpawnController : MonoBehaviour {
    public Transform roads;
    public Vehicle carPrefab;
    
    public int spawnedVehicles;
    public int successVehicles;
    public int failVehicles;
   
    void Start()
    {

    }

    public void UpdateVehicleSpawned()
    {
        spawnedVehicles += 1;
    }

    public void UpdateVehicleSucceeded()
    {
        successVehicles += 1; 
    }

    public void UpdateVehicleFailed()
    {
        failVehicles += 1; 
    }

    void Update()
    {
        if(Input.GetButtonUp("Fire3"))
        {
            Transform randomRoad = roads.RandomElement<Transform>();
            var lane = randomRoad.Cast<Transform>().Where(t => !t.GetComponent<Lane>().allowEntry).First();
            Vehicle newVehicle = (Vehicle)Instantiate(carPrefab);
            newVehicle.transform.SetParent(lane, false);
            UpdateVehicleSpawned();
        }
    }
}
