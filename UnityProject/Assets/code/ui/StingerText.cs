using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class StingerText : MonoBehaviour {
    public int timesCalled = -1;

    public string[] trafficCopNames; // replace %n in strings
    List<string> trafficCopNamesList = new List<string>();
    const string TRAFFIC_COP_TITLE_PREFIX = "Traffic Cop";

    public Text text;
    public Animation animation;

    void Start()
    {
      
      if (text == null)
      {
        text = GetComponentInChildren<Text>();
      }
    }

    public void Restart()
    {
      if (trafficCopNamesList.Count == 0)
      {
        trafficCopNamesList = trafficCopNames.ToList();
      }
      timesCalled += 1;
        if (text == null)
        {
          text = GetComponentInChildren<Text>();
        }

        if(animation != null)
        {
            animation.Play();
        }

        if(timesCalled == 1)
        {
          text.text = string.Format("{0}: {1}", TRAFFIC_COP_TITLE_PREFIX, trafficCopNamesList[0]);
          trafficCopNamesList.Remove(trafficCopNamesList[0]);
        }
        else
        {
          int selectedIndex = Random.Range(0, trafficCopNamesList.Count - 1);
          text.text = string.Format("{0} {1}: {2}", TRAFFIC_COP_TITLE_PREFIX, timesCalled, trafficCopNamesList[selectedIndex]);
          trafficCopNamesList.Remove(trafficCopNamesList[selectedIndex]);
        }
    }
}
