using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class EnterQueueState : GoingState {
    public float CAR_SEPARATION = 1f;
    Vehicle self;
    Lane selfLane; 
    StateMachine stateMachine; 

	void OnEnable () {
        self = transform.parent.parent.GetComponent<Vehicle>();
        selfLane = self.transform.parent.GetComponent<Lane>();
        stateMachine = transform.parent.GetComponent<StateMachine>(); 
	}
	
	void Update () {
	    // forward until we hit the end of the lane OR there's a car within separation in front of us 
        self.velocity += self.acceleration * Time.deltaTime;

        var currentPosition = self.transform.localPosition;
        var vehiclesAhead = self.transform.parent.Cast<Transform>()
            .Where(t =>
            {
                var notSelf = t != self.transform;
                var isAhead = directionMagnitude(t.localPosition) > directionMagnitude(self.transform.localPosition);
                var isVehicle = (t.GetComponent<Vehicle>() != null);
                //Debug.Log(string.Format("checking vehicles ahead {0} {1} {2} {3} {4}", self.transform.name, t.name, notSelf, isAhead, isVehicle));
                return notSelf && isAhead && isVehicle;
            })
            .Select(t => t.localPosition)
            .Select(t => directionMagnitude(t));
        //.OrderBy(t => t);

        List<float> vehiclesAheadSorted = new List<float>(vehiclesAhead);
        vehiclesAheadSorted.Sort();

        var separation = selfLane.carRangeMax;
        if(vehiclesAheadSorted.Any()) 
        {
            separation = vehiclesAheadSorted.First() - directionMagnitude(currentPosition);

            //Debug.Log(string.Format("checking vehicles ahead 2 {0} {1} {2} {3} {4}", self.transform.name, vehiclesAhead.First(), directionMagnitude(currentPosition), separation, CAR_SEPARATION));
            if (separation <= CAR_SEPARATION)
            {
                // if we caught up to another car, then we need to lose the velocity we've gained 
                self.velocity = 0;
                separation = directionMagnitude(currentPosition);
            }
        }

        var newPosDueToVelocity = (directionMagnitude(currentPosition) + (self.velocity * Time.deltaTime));
        var scaleOfNewPosition = Mathf.Min(newPosDueToVelocity, Mathf.Min(separation, selfLane.carRangeMax)) - directionMagnitude(currentPosition);

        self.transform.localPosition = self.transform.localPosition + (scaleOfNewPosition * Vehicle.FORWARD_DIRECTION);

        // if we hit the end of the lane wait for it to be 'go' status
        //Debug.Log(string.Format("checking range {0} {1} {2} {3}", self.transform.position, selfLane.carRangeMax, self.transform.name, vehiclesAhead.Any()));
        if (directionMagnitude(self.transform.localPosition) >= selfLane.carRangeMax)
        {
            stateMachine.GoTo("WaitToGo");
        }
	}


}
