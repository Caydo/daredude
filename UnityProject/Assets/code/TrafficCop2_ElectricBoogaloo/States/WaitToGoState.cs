using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class WaitToGoState : GoingState
{
    public int threshold = 4;
    StateMachine stateMachine;
    Road road;
    Vehicle self;
    [SerializeField] GameObject AngryIndicator = null;
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
             var isNotEnteringQueue = t.GetComponentInChildren<StateMachine>().CurrentState != "EnterQueueState";
             //Debug.Log(string.Format("checking vehicles ahead {0} {1} {2} {3} {4}", self.transform.name, t.name, notSelf, isAhead, isVehicle));
             return notSelf && isAhead && isVehicle && isNotEnteringQueue;
         })
         .Select(t => t.localPosition)
         .Select(t => directionMagnitude(t));
         //.OrderBy(t => t);

        List<float> vehiclesBehindSorted = new List<float>(vehiclesBehind);
        vehiclesBehindSorted.Sort();

        if (vehiclesBehindSorted.Count() >= (threshold - 1) && !AngryIndicator.activeInHierarchy)
        {
          AngryIndicator.SetActive(true);  
        }

        if((vehiclesBehindSorted.Count() >= threshold) || (road.currentPolicy == TrafficPolicy.Go))
        {
            stateMachine.GoTo("Go");
        }
	}
}
