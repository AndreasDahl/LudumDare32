using UnityEngine;
using System.Collections;

public class Circle : MonoBehaviour
{
    public int segments;
    public float radius;
    LineRenderer line;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        line.SetVertexCount(segments + 1);
        CreatePoints();
    }

    void Update()
    {
        CreatePoints();
    }

    void CreatePoints()
    {
        float x;
        float y;
        float z = 100f;

        float angle = 0f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            x += this.transform.position.x;
            y += this.transform.position.y;
            line.SetPosition(i, new Vector3(x, y, z));
            angle += (360f / segments);
        }
    }
}