using UnityEngine;
using System.Collections;

public class Boundry : MonoBehaviour {

	// Use this for initialization
    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("collision");
        if(other.gameObject.name != "Cube")
            Destroy(other.gameObject);
    }
}
