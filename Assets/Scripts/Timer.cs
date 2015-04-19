using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public static int STAGES = 6;
    
	public float speedMultiplier = 1f;
    public Text nextAbility;
    public int currentTrigger = 0;
    public float step, gameTick;
    public AudioSource audioPlayer;
    private List<TimerCallback> callbacks;
    private bool playing;

    private void increment()
    {
        currentTrigger += 1;
        step -= 1f;
        if (currentTrigger >= STAGES)
        {
            currentTrigger = 0;
        }
 
        foreach (TimerCallback callback in callbacks)
        {
			callback.onTime(currentTrigger);
        }
    }

	private void tick() {
		if (!playing)
		{
			audioPlayer.Play();
			playing = true;
		}
		gameTick -= 0.50f;

		foreach (TimerCallback callback in callbacks) {
			callback.onTick();
		}
	}

	// Use this for initialization
	void Awake () {
        callbacks = new List<TimerCallback>();
        playing = false;
	}
	
	// Update is called once per frame
	void Update () {
        step += Time.deltaTime * speedMultiplier;
        gameTick += Time.deltaTime * speedMultiplier;

        if (gameTick >= 0.50f && !(step >= 1.0f))
        {
			tick ();
        }
        if (step >= 1.0f) 
        {
            increment();    
        }

		foreach (TimerCallback callback in callbacks) {
			callback.onUpdate(step);
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
        bool onTime(int i);
		void onTick ();
		void onUpdate (float progress);
    }

    public void setAbilityText(string ability, Color color)
    {
        nextAbility.text = ability;
        nextAbility.color = color;
    }
}
