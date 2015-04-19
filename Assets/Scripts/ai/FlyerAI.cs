﻿using UnityEngine;
using System.Collections;

public class FlyerAI : MonoBehaviour, EnemyInterface
{
    public GameObject effect;
    public GameObject[] pickUps = new GameObject[6];
    public int direction = -1;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (direction * GetComponent<Rigidbody2D>().velocity.x < 1)
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * direction * 0.1f * 50);
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
