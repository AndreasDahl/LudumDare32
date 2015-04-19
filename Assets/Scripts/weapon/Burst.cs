using UnityEngine;
using System.Collections;

public class Burst : Weapon {
	public float lifetime;
	public AudioClip weaponSound;
    public AudioClip pickUpSound;
	public float lifetimeLeft;
    public Sprite icon;


	override public void fire(GameObject owner)
	{
		base.fire (owner);

		GameObject go =(GameObject) Instantiate(this.gameObject, owner.transform.position + new Vector3(0f, 0f, 10f) , Quaternion.identity);
		go.transform.parent = owner.transform;
		lifetimeLeft = lifetime;
	}

	void Update()
	{
		lifetimeLeft -= Time.deltaTime;
		if (lifetimeLeft <= 0) {
			Destroy(this.gameObject);
 	   }
	}
	
	override public Color getPulseColor() {
        return new Color(1f, 0f, 1f, 0.4f);
	}
	
	override public AudioClip getAudioclip()
	{
		return weaponSound;
	}

    public override string getAbilityName()
    {
        return "Burst";
    }

    public override AudioClip getPickUpAudioclip()
    {
        return pickUpSound;
    }

    public override Sprite getAbilityIcon()
    {
        return icon;
    }
}
