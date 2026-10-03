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
        //variable for storing the position on the circle it would be, incremented by the speed and time for a smooth increase
        angle += speed * Time.deltaTime;

        //resetting the angle number if it goes over 360 (resetting the circle)
        if (angle > 360)
        {
            angle = 0;
        }

        //caluclating the angle position using our storing variable
        Vector3 moonPos = new Vector3(Mathf.Cos((angle) * Mathf.Deg2Rad), Mathf.Sin((angle) * Mathf.Deg2Rad), 0);

        //moving the moon over to the target, adding the angle location, then multiplying it by radius
        transform.position = target.transform.position + moonPos * radius;
    }
}
