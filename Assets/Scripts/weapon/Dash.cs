using UnityEngine;
using System.Collections;

public class Dash : Weapon {
	private const float lifetime = 0.22f;

	public AudioClip weaponSound;
	public float speed = 40f;
	public AudioClip pickUpSound;
	private static float lifeLeft;

	override public void fire(GameObject owner)
	{
		base.fire (owner);

		Instantiate (this.gameObject);

		bool faceRight = owner.GetComponent<PlayerControl>().faceRight;
		owner.GetComponent<PlayerControl>().dashing = true;
		Vector2 v = owner.GetComponent<Rigidbody2D> ().velocity;

		owner.GetComponent<Rigidbody2D> ().velocity = new Vector2 (faceRight ? speed : -speed, v.y * 0.3f);
		lifeLeft = lifetime;

		Debug.Log ("FIRE");
	}

	void Update() {
		Debug.Log ("Life left " + lifeLeft);
		lifeLeft -= Time.deltaTime;
		if (lifeLeft < 0f) {
			Debug.Log ("Stop Dashing");
			FindObjectOfType<PlayerControl>().dashing = false;
			Destroy(this.gameObject);
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
}
