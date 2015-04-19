using UnityEngine;
using System.Collections;

public class FlyerAI : MonoBehaviour, EnemyInterface
{
    public GameObject effect;
    public GameObject[] pickUps = new GameObject[6];
    public int direction = -1;

    // Update is called once per frame
    void FixedUpdate()
    {
        this.gameObject.transform.position += new Vector3(0f, 5 * Time.deltaTime*direction, 0f);
    }

    public void doEffect()
    {
        GameObject go = (GameObject)Instantiate(effect, this.gameObject.transform.position, Quaternion.identity);
        go.GetComponent<Circle>().doExpand();
        go.GetComponent<Circle>().doDestroy(0.1f);
    }

    public void flip()
    {
        direction *= -1;
    }

    public void death()
    {
        //if (Random.Range(0, 9) > 7)
        Instantiate(pickUps[Random.Range(0, 5)], this.gameObject.transform.position, Quaternion.identity);
        FindObjectOfType<PlayerControl>().increaseScore();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
            flip();
    }
}
