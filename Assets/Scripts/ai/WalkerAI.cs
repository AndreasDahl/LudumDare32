using UnityEngine;
using System.Collections;

public class WalkerAI : MonoBehaviour {
    public GameObject effect;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += new Vector3 (-0.05f, 0.0f, 0.0f);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Cube")
        {
            Application.LoadLevel("level1");
        }
    }

    public void doEffect()
    {
        GameObject go = (GameObject) Instantiate(effect, this.gameObject.transform.position, Quaternion.identity);
        go.GetComponent<Circle>().doExpand();
        go.GetComponent<Circle>().doDestroy();
    }
}
