using UnityEngine;
using System.Collections;
using System.Linq;

public class TrafficDirector : MonoBehaviour {
    public Transform roads;
    public int currentRoad; // road cop is currently directing
    public Transform CurrentRoad()
    {
        return roads.GetChild(currentRoad);
    }

    void Update()
    {
        if(Input.GetButtonUp("Fire1")) // fire1 will be the 'rotate' button; 
            // TODO I think that we probably want it to keep rotating if held down but not too fast 
        {
            int maxIndex = roads.childCount;
            currentRoad = (currentRoad + 1 >= maxIndex) ? 0 : (currentRoad + 1);
        }

        if(Input.GetButtonUp("Fire2")) // fire 2 will be the toggle road type 
        {
            foreach(Lane l in CurrentRoad().Cast<Transform>()
                .Select(t => t.GetComponent<Lane>())
                .Where(t => t != null && !t.allowEntry)) // only lanes that allow egress need a stop/go status 
            {
                l.currentPolicy = (l.currentPolicy == TrafficPolicy.Go) ? TrafficPolicy.Stop : TrafficPolicy.Go;
            }
        }
    }
}
