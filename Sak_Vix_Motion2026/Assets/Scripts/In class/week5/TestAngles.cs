using UnityEngine;

public class TestAngles : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static float VectorToAngle(Vector3 v)
    {
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

        return angle - 90f;
    }

    public static float VectorDot(Vector3 a, Vector3 b)
    {
        float dotProd = a.x * b.x + a.y * b.y;

        return dotProd;
    }

}
