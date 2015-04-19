using UnityEngine;
using System.Collections;

public class BurstEffect : MonoBehaviour {
	public float growth;
    public float lifetimeLeft;
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
    void Update()
    {
        lifetimeLeft -= Time.deltaTime;
        if (lifetimeLeft <= 0)
        {
            Destroy(this.gameObject);
        }
		this.gameObject.transform.localScale += new Vector3(growth, growth, 1f);
	}
}
