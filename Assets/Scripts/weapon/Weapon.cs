using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
	virtual public void fire(GameObject owner) {
	}

	public abstract Color getPulseColor();

    public abstract AudioClip getAudioclip();

    public abstract AudioClip getPickUpAudioclip();

    public abstract string getAbilityName();

    public abstract Sprite getAbilityIcon();
}
