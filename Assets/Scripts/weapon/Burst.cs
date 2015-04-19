using UnityEngine;
using System.Collections;

public class Burst : Weapon {
	public float lifetime;
	public AudioClip weaponSound;
    public AudioClip pickUpSound;

    public Sprite icon;


	override public void fire(GameObject owner)
	{
		base.fire (owner);

        GameObject go = (GameObject) Resources.Load("Sphere");
        go.GetComponent<BurstEffect>().lifetimeLeft = lifetime;
        Instantiate(go, owner.transform.position + new Vector3(0f, 0f, 10f), Quaternion.identity);
	}

	void Update()
	{

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

    public override string getAbilityObjectName()
    {
        return "BurstWeapon";
    }
}
