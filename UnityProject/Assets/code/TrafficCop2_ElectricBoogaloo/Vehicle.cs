using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {
    public float acceleration;
    public float velocity; // game coords / sec

    public static Vector3 FORWARD_DIRECTION = new Vector3(0, 0, 1);
}
