using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public static int STAGES = 10;
    
	public float speedMultiplier = 1f;
    public Text uiTimer;
    public Text nextAbility;
    public Circle circle;
    public int currentTrigger;
    public float step, gameTick;
    public AudioSource audioPlayer;
    private List<TimerCallback> callbacks;
    private bool displayCircle, playing;

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
        playing = false;
	}
	
	// Update is called once per frame
	void Update () {
        step += Time.deltaTime * speedMultiplier;
        gameTick += Time.deltaTime * speedMultiplier;

        if (gameTick >= 0.50f && !(step >= 1.0f))
        {
//            if (!playing)
//            {
                audioPlayer.Play();
//                playing = true;
//            }
            gameTick -= 0.50f;
        }
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

    public void setAbilityText(string ability, Color color)
    {
        nextAbility.text = ability;
        nextAbility.color = color;
    }
}
