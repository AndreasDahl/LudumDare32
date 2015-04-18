using UnityEngine;
using System.Collections;

public class FlyerAI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += new Vector3 (0.0f, 0.0f, 0.0f);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Cube") {
			Destroy(other.transform.root.gameObject);
		}
		Debug.Log (other.name);
	}
}
