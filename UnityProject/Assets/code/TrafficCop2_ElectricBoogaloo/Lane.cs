using UnityEngine;
using System.Collections;


public class Lane : MonoBehaviour {
    public bool allowEntry; // whether cars can enter this lane
    public int indexInParent; // lane indices go from left to right in an anti-clockwise direction.
}
