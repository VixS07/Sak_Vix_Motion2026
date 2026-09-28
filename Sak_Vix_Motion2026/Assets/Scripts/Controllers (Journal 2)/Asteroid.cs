using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance;
    public Vector3 randomDirection;
    float randomX;
    float randomY;
    public Vector3 endLocation;


    // Start is called before the first frame update
    void Start()
    {
        randomX = Random.Range(-maxFloatDistance, maxFloatDistance);
        randomY = Random.Range(-maxFloatDistance, maxFloatDistance);
        endLocation = transform.position + new Vector3(randomX, randomY).normalized * maxFloatDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (randomX == 0)
        {
            randomX = Random.Range(-maxFloatDistance, maxFloatDistance);
            endLocation = transform.position + new Vector3(randomX, randomY).normalized * maxFloatDistance;
        }
        if (randomY == 0)
        {
            randomY = Random.Range(-maxFloatDistance, maxFloatDistance);
            endLocation = transform.position + new Vector3(randomX, randomY).normalized * maxFloatDistance;
        }

        if (Vector3.Distance(transform.position, endLocation) <= arrivalDistance)
        {
            AsteroidMovement();
        }
        AsteroidMovement();
    }

    public void AsteroidMovement()
    {
        
        Vector3 direction = (endLocation - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        if(Vector3.Distance(transform.position, endLocation) <= arrivalDistance)
        {
            randomX = Random.Range(-maxFloatDistance, maxFloatDistance);
            randomY = Random.Range(-maxFloatDistance, maxFloatDistance);
            endLocation = transform.position + new Vector3(randomX, randomY).normalized * maxFloatDistance;
        }
    }
}
