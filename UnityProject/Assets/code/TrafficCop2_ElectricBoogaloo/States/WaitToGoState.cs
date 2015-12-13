using UnityEngine;
using System.Collections;
using System.Linq;

public class WaitToGoState : GoingState
{
    public int threshold = 4;
    StateMachine stateMachine;
    Road road;
    Vehicle self;

	void OnEnable () {
        stateMachine = transform.parent.GetComponent<StateMachine>();
        road = transform.parent.parent.parent.parent.GetComponent<Road>();
        self = transform.parent.parent.GetComponent<Vehicle>();
	}
	
	void Update () {
        var vehiclesBehind = self.transform.parent.Cast<Transform>()
         .Where(t =>
         {
             var notSelf = t != self.transform;
             var isAhead = directionMagnitude(t.localPosition) < directionMagnitude(self.transform.localPosition);
             var isVehicle = (t.GetComponent<Vehicle>() != null);
             //Debug.Log(string.Format("checking vehicles ahead {0} {1} {2} {3} {4}", self.transform.name, t.name, notSelf, isAhead, isVehicle));
             return notSelf && isAhead && isVehicle;
         })
         .Select(t => t.localPosition)
         .Select(t => directionMagnitude(t))
         .OrderBy(t => t);

        if((vehiclesBehind.Count() >= threshold) || (road.currentPolicy == TrafficPolicy.Go))
        {
            stateMachine.GoTo("Go");
        }
	}
}
