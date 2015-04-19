using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boost : Weapon {
    public AudioClip weaponSound;
	public float lifetime;

	public float Velocity = 40f;
    public AudioClip pickUpSound;
	public Sprite icon;
	public float lifeLeft;
	
	public List<Vector3> points;

	override public void fire(GameObject owner)
	{
		base.fire (owner);

		Vector2 v = owner.GetComponent<Rigidbody2D> ().velocity;

		owner.GetComponent<Rigidbody2D> ().velocity = new Vector2 (v.x, Velocity);

		GameObject go = (GameObject)Instantiate (this.gameObject, owner.transform.position, Quaternion.identity);
		go.transform.parent = owner.transform;

		lifeLeft = lifetime;
	}

    void Start() {
		points = new List<Vector3> ();
	}

	void Update() {
		lifeLeft -= Time.deltaTime;
		if (lifeLeft < 0f) {
			FindObjectOfType<PlayerControl> ().dashing = false;
			Destroy (this.gameObject);
		} else {
			points.Add(this.gameObject.transform.position);
			if (points.Count > 1) {
				LineRenderer line = this.gameObject.GetComponent<LineRenderer>();
				line.SetVertexCount(points.Count);
				for (int i = 0; i < points.Count; i++) {
					line.SetPosition(i, points[i]);
				}
			}
		}
	}

	override public Color getPulseColor() {
        return new Color(0f, 1f, 0f, 1f);
	}

	override public AudioClip getAudioclip()
	{
        return weaponSound;
	}

    override public string getAbilityName()
    {
        return "Boost";
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
        return "BoostObject";
    }
}
