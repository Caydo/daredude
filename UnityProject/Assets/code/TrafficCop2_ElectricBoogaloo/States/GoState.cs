using UnityEngine;
using System.Collections;

public class GoState : GoingState {
    Vehicle self;
    Lane selfLane; 
    
	void OnEnable () {
        self = transform.parent.parent.GetComponent<Vehicle>();
        selfLane = self.transform.parent.GetComponent<Lane>(); 
	}
	
	void Update () {
        self.velocity += self.acceleration * Time.deltaTime;

        var currentPosition = self.transform.localPosition;

        var newPosDueToVelocity = (self.velocity * Time.deltaTime);

        self.transform.localPosition = self.transform.localPosition + (newPosDueToVelocity * Vehicle.FORWARD_DIRECTION);

        if(directionMagnitude(self.transform.localPosition) >= selfLane.successRange)
        {
            self.Succeed();
        }
	}
}
