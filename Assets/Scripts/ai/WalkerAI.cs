using UnityEngine;
using System.Collections;

public class WalkerAI : MonoBehaviour, EnemyInterface
{
    public GameObject effect;
    public GameObject[] pickUps = new GameObject[6];
    public int direction = -1;
	
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
        FindObjectOfType<PlayerControl>().increaseScore();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "PowerUp")
        {
            flip();
        }
    }
}
