using UnityEngine;
using System.Collections;

public class GoingState : MonoBehaviour{
    public float directionMagnitude(Vector3 origVector)
    {
        float retval = 0.0f;
        if (Vehicle.FORWARD_DIRECTION.x > 0)
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

    public Vector3 directionPosition(Vector3 origVector)
    {
        return Vector3.Scale(origVector, Vehicle.FORWARD_DIRECTION);
    }
}
