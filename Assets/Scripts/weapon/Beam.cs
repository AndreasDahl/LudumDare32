using UnityEngine;
using System.Collections;

public class Beam :MonoBehaviour, Weapon {
	private const float LIFETIME = 1.0f;
	
	private static float lifetime;
	
	void OnTriggerStay2D(Collider2D other)
	{
        if (other.gameObject.name == "Walker")
        {
            other.gameObject.GetComponent<WalkerAI>().doEffect();
        }
		Destroy(other.gameObject);
	}
	
	void Weapon.fire(GameObject owner)
	{
		GameObject go =(GameObject) Instantiate(this.gameObject, owner.transform.position , Quaternion.identity);
		go.transform.parent = owner.transform;
		go.transform.position += new Vector3(25.30f, 0, 0);
		lifetime = LIFETIME;
	}
	
	void Update()
	{
		if (lifetime > 0f) {
			lifetime -= Time.deltaTime;
			if (lifetime <= 0)
				Destroy(this.gameObject);
		}
	}
}
