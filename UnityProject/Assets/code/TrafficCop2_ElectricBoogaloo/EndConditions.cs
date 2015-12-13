using UnityEngine;
using System.Collections;

public abstract class EndCondition {
    virtual public bool checkSuccess(SpawnController spawner)
    {
        return false; 
    }
    virtual public bool checkFailure(SpawnController spawner)
    {
        return false;
    }
}

public class SuccessfulVehiclesCondition : EndCondition
{
    public int successesNeeded;
    override public bool checkSuccess(SpawnController spawner)
    {
        //Debug.Log(string.Format("checkSuccess {0} {1}", spawner.successVehicles().ToString(), successesNeeded.ToString()));
        return spawner.successVehicles() >= successesNeeded;
    }
}

public class TooManyFailsCondition : EndCondition
{
    public int failuresAllowed;
    override public bool checkFailure(SpawnController spawner)
    {
        //Debug.Log(string.Format("checkFail {0} {1}", spawner.failVehicles().ToString(), failuresAllowed.ToString()));
        return spawner.failVehicles() >= failuresAllowed;
    }
}