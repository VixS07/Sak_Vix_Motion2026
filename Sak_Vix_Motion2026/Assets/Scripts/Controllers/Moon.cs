using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Moon : MonoBehaviour
{
    public float radius;
    public float speed;
    public Transform target;
    float angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OrbitalMotion(radius, speed, target);
    }

    public void OrbitalMotion(float radius, float speed, Transform target)
    {
        angle += speed * Time.deltaTime;

        if (angle > 360)
        {
            angle = 0;
        }

        Vector3 moonPos = new Vector3(Mathf.Cos((angle) * Mathf.Deg2Rad), Mathf.Sin((angle) * Mathf.Deg2Rad), 0);

        transform.position = target.transform.position + moonPos * radius;
    }
}
