using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyTimer : MonoBehaviour
{
    public static int STAGES = 10;

    public Text uiTimer;
    public Text nextAbility;
    public Circle circle;
    public int currentTrigger;
    public float step, gameTick;
    public AudioSource audioPlayer;
    private List<TimerCallback> callbacks;
    private bool displayCircle;

    private void increment()
    {
        currentTrigger -= 1;
        step -= 1;
        if (currentTrigger < 0)
        {
            currentTrigger = 9;
        }

        uiTimer.text = currentTrigger.ToString();
        displayCircle = false;
        foreach (TimerCallback callback in callbacks)
        {
            displayCircle = callback.onTime(currentTrigger);

        }
    }

    // Use this for initialization
    void Awake()
    {
        uiTimer.text = "9";
        callbacks = new List<TimerCallback>();
    }

    // Update is called once per frame
    void Update()
    {
        step += Time.deltaTime;
        gameTick += Time.deltaTime;

        if (gameTick >= 0.50f && !(step >= 1.0f))
        {
            //audioPlayer.Play();
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
