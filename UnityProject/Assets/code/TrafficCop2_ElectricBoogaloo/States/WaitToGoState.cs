using UnityEngine;
using System.Collections;

public class WaitToGoState : MonoBehaviour {
    StateMachine stateMachine;
    Road road; 

	void OnEnable () {
        stateMachine = transform.parent.GetComponent<StateMachine>();
        road = transform.parent.parent.parent.parent.GetComponent<Road>(); 
	}
	
	void Update () {
        if(road.currentPolicy == TrafficPolicy.Go)
        {
            stateMachine.GoTo("Go");
        }
	}
}
