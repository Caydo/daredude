using UnityEngine;
using System.Collections;
using System.Linq;

public class EnterQueueState : GoingState {
    public float CAR_SEPARATION = .05f;
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

        var currentPosition = self.transform.position;
        var vehiclesAhead = self.transform.parent.Cast<Transform>()
            .Where(t => ((directionPosition(currentPosition) - directionPosition(t.position)).sqrMagnitude < 0)
                && (t.GetComponent<Vehicle>() != null))
            .Select(t => t.position)
            .Select(t => directionMagnitude(t))
            .OrderBy(t => t);

        var separation = selfLane.carRangeMax;
        if(vehiclesAhead.Any()) 
        {
            separation = Mathf.Max(0, vehiclesAhead.First() - directionMagnitude(currentPosition) - CAR_SEPARATION);
        }

        var newPosDueToVelocity = (directionMagnitude(currentPosition) + (self.velocity * Time.deltaTime));
        var scaleOfNewPosition = Mathf.Min(newPosDueToVelocity, Mathf.Min(separation, selfLane.carRangeMax)) - directionMagnitude(currentPosition);

        self.transform.position = self.transform.position + (scaleOfNewPosition * Vehicle.FORWARD_DIRECTION);

        // if we hit the end of the lane wait for it to be 'go' status
        //Debug.Log(string.Format("checking range {0} {1}", self.transform.position, selfLane.carRangeMax));
        if (directionMagnitude(self.transform.position) >= selfLane.carRangeMax)
        {
            stateMachine.GoTo("WaitToGo");
        }
        // if we hit another car, then we need to lose acceleration
        else if(vehiclesAhead.Any() && directionMagnitude(self.transform.position) >= (vehiclesAhead.First() - CAR_SEPARATION))
        {
            self.acceleration = 0;
        }
	}


}
