using UnityEngine;
using System.Collections;

public class CollideState : MonoBehaviour {
    Vehicle self;

    float explodeTimeRemaining; 

	void OnEnable () {
        self = transform.parent.parent.GetComponent<Vehicle>();

        explodeTimeRemaining = 1f;
	}
	
	void Update () {
        explodeTimeRemaining -= Time.deltaTime;
        if(explodeTimeRemaining <= 0)
        {
            self.Fail();
        }
	}
}
