using UnityEngine;
using System.Collections;


public enum TrafficPolicy
{
    Stop = 0, Go = 1//, Yield?
}

public class Road : MonoBehaviour {
    public TrafficPolicy currentPolicy; 
}
