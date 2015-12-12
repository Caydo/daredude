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

        var currentPosition = self.transform.position;

        var newPosDueToVelocity = (self.velocity * Time.deltaTime);

        self.transform.position = self.transform.position + (newPosDueToVelocity * Vehicle.FORWARD_DIRECTION);

        if(directionMagnitude(self.transform.position) >= selfLane.successRange)
        {
            self.Succeed();
        }
	}
}
