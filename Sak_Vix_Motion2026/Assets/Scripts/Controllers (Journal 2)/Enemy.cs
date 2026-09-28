using System.Collections;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    public Transform player;

    float t;
    public float acceleration;
    public float speed;
    public float accelerationRate;
  
    public Vector3 currentVelocity;
    public float maxSpeed;
    private void Start()
    {
        acceleration = speed / accelerationRate;

    }

    private void Update()
    {
        ChasePlayer();
    }

    public void ChasePlayer()
    {
        t += Time.deltaTime;

         if ( t >= 1)
        {
            Vector3 direction = player.transform.position - transform.position;
            currentVelocity += direction.normalized * acceleration * Time.deltaTime;
            if (currentVelocity.x >= maxSpeed)
            {
                currentVelocity.x = maxSpeed;
            }
            if (currentVelocity.y >= maxSpeed)
            {
                currentVelocity.y = maxSpeed;
            }
            if (currentVelocity.x <= -maxSpeed)
            {
                currentVelocity.x = -maxSpeed;
            }
            if (currentVelocity.y <= -maxSpeed)
            {
                currentVelocity.y = -maxSpeed;
            }
            transform.position += currentVelocity * Time.deltaTime;
        }

    }
}


