using UnityEngine;
using System.Collections;

public class PowerUpSpeed : MonoBehaviour {

	void OnTriggerStay2D(Collider2D other)
	{
        if (other.gameObject.tag == "Player")
        {
		    Destroy (this.gameObject);
		    powerUp (other.gameObject);
        }
	}
	
	private void powerUp(GameObject target) {
		target.GetComponent<PlayerControl>().timer.speedMultiplier = 2f;
	}
}
