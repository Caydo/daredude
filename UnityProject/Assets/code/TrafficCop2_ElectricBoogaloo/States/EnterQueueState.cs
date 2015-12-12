using UnityEngine;
using System.Collections;
using System.Linq;

public class EnterQueueState : MonoBehaviour {
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
        if (directionMagnitude(self.transform.position) > selfLane.carRangeMax)
        {
            stateMachine.GoTo("WaitToGo");
        }
	}

    float directionMagnitude(Vector3 origVector)
    {
        float retval = 0.0f;
        if(Vehicle.FORWARD_DIRECTION.x > 0)
        {
            retval = origVector.x;
        }
        else if (Vehicle.FORWARD_DIRECTION.y > 0)
        {
            retval = origVector.y;
        }
        else
        {
            retval = origVector.z;
        }
        return retval;
    }
    Vector3 directionPosition(Vector3 origVector)
    {
        return Vector3.Scale(origVector, Vehicle.FORWARD_DIRECTION);
    }
}
