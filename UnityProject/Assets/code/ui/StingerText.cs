using UnityEngine;
using System.Collections;

public class StingerText : MonoBehaviour {
    public int state;
    public float initialPause;
    public float fadeIn;
    public float fadeDown;
    public float hold; 
    public float fadeOut;
    public int timesCalled = -1;

    public string[] trafficCopNames; // replace %n in strings

    TextMesh text; 

    void Start()
    {
        text = GetComponent<TextMesh>();
        Restart();
    }

    public void Restart()
    {
        state = 0;
        initialPause = .5f;
        fadeIn = 1f;
        fadeDown = .2f;
        hold = 5f;
        fadeOut = 1f;
        timesCalled += 1;

        text.text = trafficCopNames[timesCalled].Replace(":", ":\n");
    }

	void Update () {
        var newState = state;
        switch(state)
        {
            case 0:
                {
                    transform.LookAt(
                        transform.position + Camera.main.transform.rotation * Vector3.back * -1.0f,
                        Camera.main.transform.rotation * Vector3.up
                    );
                    newState = 1;
                    break;
                }
            case 1:
                {
                    initialPause -= Time.deltaTime; 
                    if(initialPause <= 0)
                    {
                        newState = 2; 
                    }
                    break;
                }
            case 2:
                {
                    var curColor = text.color;
                    fadeIn -= Time.deltaTime;
                    curColor.a = ((1f - fadeIn) / 1f);
                    text.color = curColor; 
                    if(fadeIn <= 0)
                    {
                        newState = 3;
                    }
                    break;
                }
            case 3:
                {
                    fadeDown -= Time.deltaTime;
                    var curColor = text.color;
                    curColor.a = ((.8f + fadeDown) / 1f);
                    text.color = curColor;
                    if(fadeDown <= 0)
                    {
                        newState = 4;
                    }
                    break;
                }
            case 4:
                {
                    hold -= Time.deltaTime; 
                    if(hold <= 0)
                    {
                        newState = 5;
                    }
                    break;
                }
            case 5:
                {
                    fadeOut -= Time.deltaTime;
                    var curColor = text.color;
                    curColor.a = (Mathf.Min(.8f, fadeOut) / 1f);
                    text.color = curColor;
                    if(fadeOut <= 0)
                    {
                        newState = 6; 
                    }
                    break; 
                }
            default:
                {
                    // nothing 
                    break;
                }
        }

        state = newState;
	}
}
