using UnityEngine;
using System.Collections;
using System.Linq;

public class Vehicle : MonoBehaviour {
    public float acceleration;
    public float velocity; // game coords / sec

    public static Vector3 FORWARD_DIRECTION = new Vector3(0, 0, 1);

    StateMachine stateMachine; 

    void Start()
    {
        stateMachine = transform.Cast<Transform>()
            .Select(t => t.GetComponent<StateMachine>())
            .Where(t => t != null)
            .First();
    }

    public void Succeed()
    {
        // TODO add to our tally of successful vehicles and stuff? 
        GameObject.Destroy(gameObject);
    }
    public void Fail()
    {
        // TODO add to tally, etc
        GameObject.Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(string.Format("Vehicle Collision! {0} {1}", transform.name, other.name));
        var otherVehicle = other.gameObject.GetComponent<Vehicle>();
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
