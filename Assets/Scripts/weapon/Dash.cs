using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dash : Weapon {
	private const float lifetime = 0.22f;

	public AudioClip weaponSound;
	public float speed = 40f;
	public AudioClip pickUpSound;
    public Sprite icon;
	public float lifeLeft;
	public List<Vector3> points;

	override public void fire(GameObject owner)
	{
		base.fire (owner);

		GameObject go = (GameObject)Instantiate (this.gameObject, owner.transform.position + new Vector3(0f, 0f, 0f) , Quaternion.identity);
		go.transform.parent = owner.transform;
		
		bool faceRight = owner.GetComponent<PlayerControl>().faceRight;
		owner.GetComponent<PlayerControl>().dashing = true;
		Vector2 v = owner.GetComponent<Rigidbody2D> ().velocity;

		owner.GetComponent<Rigidbody2D> ().velocity = new Vector2 (faceRight ? speed : -speed, v.y * 0.3f);
		lifeLeft = lifetime;

		points.Add (owner.gameObject.transform.position);
	}

	void Start() {
		points = new List<Vector3>();
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
		return new Color(1f, 0.5f, 0f, 0.5f);
	}

	override public AudioClip getAudioclip()
	{
		return weaponSound;
	}
	
	override public string getAbilityName()
	{
		return "Dash";
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
        return "DashWeapon";
    }
}
