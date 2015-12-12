using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq; 

public class VictoryConditionPanel : MonoBehaviour {
    Text success;
    Text fail;
    Text total;
    SpawnController game; 

	void Start () {
        game = Finder.Find<SpawnController>("GameController"); 
        foreach(Text child in transform.Cast<Transform>()
            .Select(t => t.GetComponent<Text>())
            .Where(t => t != null))
        {
            if(child.transform.name == "Success")
            {
                success = child;
            } else if (child.transform.name == "Failure")
            {
                fail = child; 
            } else if(child.transform.name == "Total")
            {
                total = child; 
            }
        }
	}
	
	void Update () {
        success.text = game.successVehicles().ToString();
        fail.text = game.failVehicles().ToString();
        total.text = game.spawnedVehicles().ToString(); 
	}
}
