using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public float spawnInterval;
    public GameObject enemy;
    private float time;

    void Start()
    {
        Instantiate(enemy, this.gameObject.transform.position, Quaternion.identity);
    }

	void Update () {
        time += Time.deltaTime;
        if ((spawnInterval % time) == spawnInterval)
        {
            time = 0f;
            Instantiate(enemy, this.gameObject.transform.position, Quaternion.identity);
        }
	}
}
