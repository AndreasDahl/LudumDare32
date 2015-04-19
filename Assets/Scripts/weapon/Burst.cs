﻿using UnityEngine;
using System.Collections;

public class Burst : Weapon {
	public float growth = 0.2f;
	public float lifetime = 0.3f;
	public AudioClip weaponSound;
    public AudioClip pickUpSound;
	private static float lifetimeLeft;
    public Sprite icon;

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
            other.gameObject.GetComponent<EnemyInterface>().doEffect();
            other.gameObject.GetComponent<EnemyInterface>().death();
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
