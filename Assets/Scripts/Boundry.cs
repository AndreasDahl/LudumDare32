using UnityEngine;
using System.Collections;

public class Boundry : MonoBehaviour {

	// Use this for initialization
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name != "Cube" && other.gameObject.name != "Floor") {
			if (other.gameObject.tag == "Enemy") { 
				other.gameObject.GetComponent<WalkerAI> ().flip ();
			} else if (other.gameObject.tag == "Projectile") {
				Destroy (other.gameObject);
			}
		}
    }
}
