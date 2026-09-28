using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AngleTest : MonoBehaviour
{
    public List<float> degrees;
    int i = 0;
    public float r = 2;
    public Vector3 startPos = Vector3.zero;
    public float t;
    public Vector3 circleOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float fortyFiveDegrees = 45f;

        float ffdInRadians = fortyFiveDegrees * Mathf.Deg2Rad;


        float twoPiRadians = 2 * Mathf.PI;

        float tprInDegrees = twoPiRadians * Mathf.Rad2Deg;

        float currentAngle = 90f;
        Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        Mathf.Sin(currentAngle * Mathf.Deg2Rad);


    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        float currentAngle = degrees[i] * Mathf.Deg2Rad;
        Vector3 point = new Vector3 (Mathf.Cos(currentAngle), Mathf.Sin(currentAngle)) * r + circleOffset;

        if (t>=0.5)
        {
            i++;
            if(i >= degrees.Count)
            {
                i = 0;
            }
            t = 0;
        }

        Debug.DrawLine(startPos + circleOffset, startPos + point, Color.purple);
    }
}
