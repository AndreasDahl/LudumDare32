using UnityEngine;
using System.Collections;

public class Beam : Weapon {
	private const float LIFETIME = 0.44f;
	
	private static float lifetime;

    public AudioClip weaponSound;

    private string name = "Beam";
	
	void OnTriggerStay2D(Collider2D other)
	{
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<WalkerAI>().doEffect();
			Destroy(other.gameObject);
		}
	}
	
	override public void fire(GameObject owner)
	{
		base.fire (owner);

		GameObject go =(GameObject) Instantiate(this.gameObject, owner.transform.position , Quaternion.identity);
		go.transform.parent = owner.transform;
        go.transform.position += new Vector3(0, 0, 10f); //25.30f
		lifetime = LIFETIME;
	}

	override public Color getPulseColor() {
        return new Color(1f, 1f, 0f, 2f);
	}
	
	void Update()
	{
		if (lifetime > 0f) {
			lifetime -= Time.deltaTime;
			if (lifetime <= 0)
				Destroy(this.gameObject);
		}
	}

    override public AudioClip getAudioclip()
    {
        return weaponSound;
    }

    override public string getAbilityName()
    {
        return name;
    }
}
