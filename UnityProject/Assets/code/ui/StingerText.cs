using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StingerText : MonoBehaviour {
    public int timesCalled = -1;

    public string[] trafficCopNames; // replace %n in strings

    public Text text;
    public Animation animation;

    void Start()
    {
        if (text == null)
        {
            text = GetComponentInChildren<Text>();
        }
        Restart();
    }

    public void Restart()
    {
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
            text.text = trafficCopNames[timesCalled].Replace(":", ":\n");
        }
        else
        {
            var selectedIndex = 0;
            do {
                selectedIndex = Random.Range(2, 9);
            } while (trafficCopNames[selectedIndex] == ""); 

            text.text = trafficCopNames[selectedIndex].Replace(":", ":\n").Replace("%n", timesCalled.ToString());
            trafficCopNames[selectedIndex] = "";
        }
    }
}
