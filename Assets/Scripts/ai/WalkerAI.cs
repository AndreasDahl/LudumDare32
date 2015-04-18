using UnityEngine;
using System.Collections;

public class WalkerAI : MonoBehaviour {
    public GameObject effect;
    public GameObject pickUp;
    public int direction = -1;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if (direction * GetComponent<Rigidbody2D>().velocity.x < 1)
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * direction * 3f * 200);
	}

    public void doEffect()
    {
        GameObject go = (GameObject) Instantiate(effect, this.gameObject.transform.position, Quaternion.identity);
        go.GetComponent<Circle>().doExpand();
        go.GetComponent<Circle>().doDestroy(0.1f);
    }

    public void flip()
    {
        direction *= -1;
    }

    public void death()
    {
        Instantiate(pickUp, this.gameObject.transform.position, Quaternion.identity);
    }
}
