using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public static int STAGES = 10;
    
    public Text uiTimer;
    public Circle circle;
    public int currentTrigger;
    public float step;

    private List<TimerCallback> callbacks;
    private bool displayCircle;

    private void increment()
    {
        currentTrigger += 1;
        step -= 1;
        if (currentTrigger >= STAGES)
        {
            currentTrigger = 0;
        }
 
        uiTimer.text = currentTrigger.ToString();
        displayCircle = false;
        foreach (TimerCallback callback in callbacks)
        {
            displayCircle = callback.onTime(currentTrigger);
            
        }


    }

	// Use this for initialization
	void Awake () {
        uiTimer.text = "0";
        callbacks = new List<TimerCallback>();
	}
	
	// Update is called once per frame
	void Update () {
        step += Time.deltaTime;
        if (step >= 1.0f) 
        {
            increment();    
        }
        if (displayCircle)
            circle.radius = 64 * step;
	}

    public void addCallback(TimerCallback callback)
    {
        callbacks.Add(callback);
    }

    public void removeCallback(TimerCallback callback)
    {
        callbacks.Remove(callback);
    }

    public interface TimerCallback
    {
        bool onTime(int i); 
    }
}
