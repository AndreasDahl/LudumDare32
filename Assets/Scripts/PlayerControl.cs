using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour, Timer.TimerCallback
{
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 6f;				// The fastest the player can travel in the x axis.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	public Transform groundCheck;			// A position marking where to check if the player is grounded.
    private bool grounded = false, hasPlayed;			// Whether or not the player is grounded.
    public Timer timer;
    public GameObject[] weapons = new GameObject[Timer.STAGES];
    public AudioSource audioPlayer;
    public AudioClip goalSound;
    public Image GrooveBar;
    public Image abilityQue;
    public Sprite defaultIcon;
    public Image[] abilityQueWheel = new Image[6];
    private string currentLevel = "level1";
    private float nextLevelDelay = 1f, nextLevelDelayStart = 0f;
	public bool dashing = false;
	public bool faceRight = true;
    int nextAbilityNr;
    public Text score;
    public int scoreInt = 0;
	public Circle timerCircle;
	private float timerProgress;

	bool bounce {
		get;
		set;
	}

    void Start(){
        this.gameObject.SetActive(true);
        int nextAbilityNr = 0; 
        hasPlayed = false;
        timer.addCallback(this);
        for (int i = 0; i < 6; i++)
        {
            if (weapons[i] != null)
                abilityQueWheel[i].sprite = weapons[i].GetComponent<Weapon>().getAbilityIcon();
        }
    }

	void OnDestroy() {
		timer.removeCallback (this);
	}

	void Update()
	{
        if (hasPlayed){
            nextLevelDelayStart += Time.deltaTime;
            if (nextLevelDelayStart >= nextLevelDelay)
            {
                Application.LoadLevel(currentLevel);
            }
        }
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  

		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetButtonDown("Jump") && grounded)
			jump = true;

		// Awesome bounce effect!
		if (bounce) {
			bounce = false;
			abilityQue.transform.localScale = new Vector3 (1.05f, 1.05f, 1f);
		} else {
			abilityQue.transform.localScale = new Vector3 (1f, 1f, 1f);
		}

		float radius = 64 * (1f - timerProgress);
		timerCircle.radius = radius;
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
			faceRight = h >= 0; // Set Facing
        if (h != 0)
        {

		    // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		    if(!dashing && h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
			    // ... add a force to the player.
			    GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
		
		    // If the player's horizontal velocity is greater than the maxSpeed and not dashing...
		    if(!dashing && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
			    // ... set the player's velocity to the maxSpeed in the x axis.
			    GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else if(grounded)
        {
			if (!dashing)
            	GetComponent<Rigidbody2D>().velocity = new Vector2(0f, GetComponent<Rigidbody2D>().velocity.y);
        }
	}

    bool Timer.TimerCallback.onTime(int i)
    {
        nextAbilityNr = i + 1;
        if (nextAbilityNr >= 6)
            nextAbilityNr -= 6;
        abilityQueWheel[i].color = new Color(1f,1f,1f,0.5f);
        abilityQueWheel[nextAbilityNr].color = new Color(1f, 1f, 1f, 1f);
        if (weapons[nextAbilityNr] != null)
        {
            abilityQue.sprite = weapons[nextAbilityNr].GetComponent<Weapon>().getAbilityIcon();
            timer.setAbilityText(weapons[nextAbilityNr].GetComponent<Weapon>().getAbilityName(), weapons[nextAbilityNr].GetComponent<Weapon>().getPulseColor());
			timerCircle.setColor(weapons[nextAbilityNr].GetComponent<Weapon>().getPulseColor());
		}
        else
        {
            abilityQue.sprite = defaultIcon;
            timer.setAbilityText("", new Color());
			timerCircle.setColor(new Color(1f, 1f, 1f));
        }
        if (weapons[i] != null)
        {
            weapons[i].GetComponent<Weapon>().fire(this.gameObject);
            audioPlayer.PlayOneShot(weapons[i].GetComponent<Weapon>().getAudioclip());
            return true;
        }
		return false;
    }

	void Timer.TimerCallback.onTick() {
		bounce = true;
	}

	void Timer.TimerCallback.onUpdate(float progress) {
		timerProgress = progress;
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.tag == "Enemy")
        {
            Application.LoadLevel(currentLevel);
        }
        else if (other.collider.gameObject.tag == "PowerUp")
        {
            weapons[nextAbilityNr] = other.collider.gameObject;
            abilityQue.sprite = weapons[nextAbilityNr].GetComponent<Weapon>().getAbilityIcon();
            timer.setAbilityText(weapons[nextAbilityNr].GetComponent<Weapon>().getAbilityName(), weapons[nextAbilityNr].GetComponent<Weapon>().getPulseColor());
            abilityQueWheel[nextAbilityNr].sprite = weapons[nextAbilityNr].GetComponent<Weapon>().getAbilityIcon();
            audioPlayer.PlayOneShot(other.gameObject.GetComponent<Weapon>().getPickUpAudioclip());
            other.gameObject.SetActive(false);
        }
        else if (other.collider.gameObject.name == "Goal")
        {
            audioPlayer.PlayOneShot(goalSound);
            currentLevel = other.gameObject.GetComponent<goal>().getNextLevel();
            hasPlayed = true;
        }
    }

    public void increaseScore()
    {
        scoreInt++;
        score.text = scoreInt.ToString();
    }
}
