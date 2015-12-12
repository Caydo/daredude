using UnityEngine;
using System.Collections;
using System.Linq;

public class Vehicle : MonoBehaviour {
    public float acceleration;
    public float velocity; // game coords / sec

    public static Vector3 FORWARD_DIRECTION = new Vector3(0, 0, 1);

    StateMachine stateMachine;
    SpawnController game; 

    void Start()
    {
        stateMachine = transform.Cast<Transform>()
            .Select(t => t.GetComponent<StateMachine>())
            .Where(t => t != null)
            .First();
        game = Finder.Find<SpawnController>("GameController");
    }

    public void Succeed()
    {
        game.UpdateVehicleSucceeded();
        GameObject.Destroy(gameObject);
    }
    public void Fail()
    {
        game.UpdateVehicleFailed();
        GameObject.Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(string.Format("Vehicle Collision! {0} {1}", transform.name, other.name));
        var otherVehicle = other.gameObject.GetComponent<Vehicle>();
        if (otherVehicle != null) // ignore collision with other things than vehicles
        {
            var otherStateMachine = otherVehicle.transform.Cast<Transform>()
                .Select(t => t.GetComponent<StateMachine>())
                .Where(t => t != null)
                .First();
            if (otherStateMachine.CurrentState == "Go")
            {
                otherStateMachine.GoTo("Collide");
                stateMachine.GoTo("Collide");
            }
        }
    }
}
