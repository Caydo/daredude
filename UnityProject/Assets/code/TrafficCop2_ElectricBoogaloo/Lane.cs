using UnityEngine;
using System.Collections;

public enum TrafficPolicy
{
    Stop=0, Go=1//, Yield?
}
public class Lane : MonoBehaviour {
    public TrafficPolicy currentPolicy; 
    public bool allowEntry; // whether cars can enter this lane
    public int indexInParent; // lane indices go from left to right in an anti-clockwise direction.
}
