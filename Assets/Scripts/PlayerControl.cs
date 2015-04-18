﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour, Timer.TimerCallback
{
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.


	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.


	public Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.

    public Timer timer;

    public GameObject[] weapons = new GameObject[Timer.STAGES];

    public AudioSource audioPlayer;

    void Awake()
    {
        timer.addCallback(this);
    }

	void OnDestroy() {
		timer.removeCallback (this);
	}

	void Update()
	{
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  

		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetButtonDown("Jump") && grounded)
			jump = true;
	}


	void FixedUpdate ()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");
		// If the player should jump...
        if (jump)
        {
            // Add a vertical force to the player.
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }
        if (h != -1 && h != 1 && h != 0)
            h = 0;
        if (h != 0)
        {

		    // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		    if(h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
			    // ... add a force to the player.
			    GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);

		    // If the player's horizontal velocity is greater than the maxSpeed...
		    if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
			    // ... set the player's velocity to the maxSpeed in the x axis.
			    GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else if(grounded)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
	}

    bool Timer.TimerCallback.onTime(int i)
    {
        if (weapons[i] != null)
        {
            weapons[i].GetComponent<Weapon>().fire(this.gameObject);
            audioPlayer.PlayOneShot(weapons[i].GetComponent<Weapon>().getAudioclip());
            return true;
        }
        return false;
    }
}
