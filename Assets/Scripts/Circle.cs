using UnityEngine;
using System.Collections;

public class Circle : MonoBehaviour
{
    public int segments;
    public float radius;
    LineRenderer line;
    public float step;
    public bool expandCircle, doDestroyObject;
    private const float LIFETIME = 1.0f;
    private static float lifetime;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        line.SetVertexCount(segments + 1);
		line.material = new Material (Shader.Find("Particles/Additive"));
        CreatePoints();
    }

    void Update()
    {
        CreatePoints();
        if (expandCircle)
        {
            step += Time.deltaTime;
            radius = 64 * step;
        }
        if (doDestroyObject)
        {
            if (lifetime > 0f)
            {
                lifetime -= Time.deltaTime;
                if (lifetime <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }
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

    public void doExpand()
    {
        lifetime = LIFETIME;
        expandCircle = true;
    }

	public void setColor(Color color) {
		line.SetColors (color, color);
	}

    public void doDestroy()
    {
        doDestroyObject = true;
    }
}