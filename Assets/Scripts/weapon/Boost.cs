using UnityEngine;
using System.Collections;

public class Boost : Weapon {
    public AudioClip weaponSound;
	override public void fire(GameObject owner)
	{
		base.fire (owner);

		owner.GetComponent<PlayerControl>().jump = true;
	}

	override public Color getPulseColor() {
		return new Color (0f, 1f, 0f);
	}

	override public AudioClip getAudioclip()
	{
        return weaponSound;
	}



}
