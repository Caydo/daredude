using UnityEngine;
using System.Collections;

public class StateMachine : MonoBehaviour {

    public string StartState;
    public string CurrentState;

	// Use this for initialization
	void Start () {
        GoTo(StartState);
	}

    void Update()
    {
        string state = CurrentState;
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(child.name == state);
        }
    }

    public void GoTo(string state)
    {
        //Debug.Log(string.Format("FSM: Request Transition From {0} to {1}", CurrentState, state));
        var possibleChild = gameObject.transform.Find(state);
        if (possibleChild != null)
        {
            CurrentState = state;
        }
        else
        {
            Debug.LogWarning(string.Format("Unknown state {0}", state));
        }
    }
}
