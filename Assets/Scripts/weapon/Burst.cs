using UnityEngine;
using System.Collections;

public class Burst : Weapon {
    private string name = "Burst";
	public float growth = 0.2f;
	public float lifetime = 0.3f;
	public AudioClip weaponSound;
	private static float lifetimeLeft;

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.name == "Walker")
		{
			other.gameObject.GetComponent<WalkerAI>().doEffect();
			Destroy(other.gameObject);
		}
	}

	override public void fire(GameObject owner)
	{
		base.fire (owner);

		GameObject go =(GameObject) Instantiate(this.gameObject, owner.transform.position + new Vector3(0f, 0f, 10f) , Quaternion.identity);
		go.transform.parent = owner.transform;
		lifetimeLeft = lifetime;
	}

	void Update()
	{
		if (lifetimeLeft > 0f) {
			lifetimeLeft -= Time.deltaTime;
			this.gameObject.transform.localScale += new Vector3(growth, growth, growth);
			if (lifetimeLeft <= 0) {
				Destroy(this.gameObject);
     	   }
        }
	}
	
	override public Color getPulseColor() {
		return new Color (1f, 0f, 1f);
	}
	
	override public AudioClip getAudioclip()
	{
		return weaponSound;
	}

    public override string getAbilityName()
    {
        return name;
    }
}
