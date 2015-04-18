using UnityEngine;
using System.Collections;

public class Boost : Weapon {
    public AudioClip weaponSound;
    public AudioClip pickUpSound;
    private string name = "Boost";
	override public void fire(GameObject owner)
	{
		base.fire (owner);

		owner.GetComponent<PlayerControl>().jump = true;
	}

	override public Color getPulseColor() {
        return new Color(0f, 1f, 0f, 2f);
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
}
