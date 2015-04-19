using UnityEngine;
using System.Collections;

public class Beam : Weapon {
	private const float LIFETIME = 0.44f;
	
	private static float lifetime;

    public AudioClip weaponSound;
    public AudioClip pickUpSound;
    public Sprite icon;
    private string name = "Beam";
	

	
	override public void fire(GameObject owner)
	{
		base.fire (owner);

		GameObject go =(GameObject) Instantiate(this.gameObject, owner.transform.position , Quaternion.identity);
		go.transform.parent = owner.transform;
        go.transform.position += new Vector3(0f, 0f, 10f);
		lifetime = LIFETIME;
	}

	override public Color getPulseColor() {
        return new Color(1f, 1f, 0f, 0.4f);
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
        return "BeamObject";
    }
}
