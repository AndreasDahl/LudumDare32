using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
    public float spawnInterval;
    public GameObject enemy;
    private float time;
    public bool flip;
    public List<GameObject> currentlySpawned = new List<GameObject>();

    void Start()
    {
        GameObject go = (GameObject)Instantiate(enemy, this.gameObject.transform.position, Quaternion.identity);
        currentlySpawned.Add(go);
        if (flip)
            go.GetComponent<EnemyInterface>().flip();
    }

	void Update () {
        for (int i = 0; i < currentlySpawned.Count; i++)
        {
            if (currentlySpawned[i] == null)
                currentlySpawned.RemoveAt(i);
        }
                
        time += Time.deltaTime;
        if(currentlySpawned.Count < 3){
            if ((spawnInterval % time) == spawnInterval)
            {
                time = 0f;
                GameObject go = (GameObject) Instantiate(enemy, this.gameObject.transform.position, Quaternion.identity);
                currentlySpawned.Add(go);
                if (flip)
                    go.GetComponent<EnemyInterface>().flip();
            }
        }
	}
}
