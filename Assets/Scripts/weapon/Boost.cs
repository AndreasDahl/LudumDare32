using UnityEngine;
using System.Collections;

public class Boost : Weapon {
    public AudioClip weaponSound;

	public float Velocity = 40f;
    public AudioClip pickUpSound;
    private string name = "Boost";
    public Sprite icon;
	override public void fire(GameObject owner)
	{
		base.fire (owner);

		Vector2 v = owner.GetComponent<Rigidbody2D> ().velocity;

		owner.GetComponent<Rigidbody2D> ().velocity = new Vector2 (v.x, 30f);
	}

	override public Color getPulseColor() {
        return new Color(0f, 1f, 0f, 0.4f);
	}

	override public AudioClip getAudioclip()
	{
        return weaponSound;
	}

    override public string getAbilityName()
    {
        return name;
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
