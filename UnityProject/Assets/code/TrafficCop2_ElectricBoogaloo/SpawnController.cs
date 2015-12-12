using UnityEngine;
using System.Collections;
using System.Linq; 

public class SpawnController : MonoBehaviour {
    public Transform roads;
    public Vehicle carPrefab;

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetButtonUp("Fire3"))
        {
            Transform randomRoad = roads.RandomElement<Transform>();
            var lane = randomRoad.Cast<Transform>().Where(t => !t.GetComponent<Lane>().allowEntry).First();
            Vehicle newVehicle = (Vehicle)Instantiate(carPrefab);
            newVehicle.transform.SetParent(lane, false);
        }
    }
}
