using UnityEngine;
using System.Collections;

public class BeamProjectile : MonoBehaviour {
	public float speed;

	private float lifetimeLeft;

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			other.gameObject.GetComponent<EnemyInterface>().doEffect();
            other.gameObject.GetComponent<EnemyInterface>().death();
			Destroy(other.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.position += new Vector3 (speed * Time.deltaTime, 0f, 0f);
	}
}
