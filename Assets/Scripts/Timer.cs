using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public int MAX_TRIGGER = 9;
    
    public Text uiTimer;
    public int currentTrigger;
    public float step;

    private List<TimerCallback> callbacks;

    private void increment()
    {
        currentTrigger += 1;
        step -= 1;
        if (currentTrigger > MAX_TRIGGER)
        {
            currentTrigger = 0;
        }
 
        uiTimer.text = currentTrigger.ToString();
        foreach (TimerCallback callback in callbacks)
        {
            callback.onTime(currentTrigger);
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
        void onTime(int i); 
    }
}
