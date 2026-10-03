using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;

    //Task 1 variables
    //a
    public Vector3 bombOffset;
    //b
    public int numberOfTrailBombs;
    public float bombTrailSpacing;
    public float setOfBombs = 0;
    //task 2 variables
    public int distance;
    //task 3
    public float ratio;
    //task4
    public float inMaxRange = 2.5f;
    public List<Transform> inAsteroids;

    //week 3
    public Vector3 currentVelocity;
    public float speed;
    public float accelerationTime;
    public float decelerationTime;
    public float currentAcceleration;

    public float maxSpeed;
    float deceleration;


    //week 4
    public int radius;
    public int circlePoints;
    public int numberOfPowerups;
    public GameObject powerUpPrefab;

    void Start()
    {
        currentAcceleration = speed / accelerationTime;
        deceleration = speed / decelerationTime;
    }

    void Update()
    {
        //week 4
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            SpawnPowerups(radius, numberOfPowerups);
        }
        //EnemyRadar(radius, circlePoints);

        //week 3
        PlayerMovement();

        //Journal 2 
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            //adds the bombOffset to the players position
            Vector3 offset = transform.position + bombOffset;
            //passes the offset into the SpawnBombAtOffSet method
            SpawnBombAtOffSet(offset);
        }
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            WarpPlayer(enemyTransform, ratio);
        }
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            SpawnBombTrail(bombTrailSpacing, numberOfTrailBombs);
        }
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            SpawnBombOnRandomCorner(distance);
        }
        //https://stackoverflow.com/questions/66733504/get-a-list-of-transforms-in-unity

        inAsteroids = GameObject.FindGameObjectsWithTag("Asteroid").Select(go => go.transform).ToList();
        DetectAsteroids(inMaxRange, inAsteroids);
    }
    //Task 1
    //a
    public void SpawnBombAtOffSet(Vector2 inOffSet)
    {
        GameObject bomb = Instantiate(bombPrefab);
        bomb.transform.position = inOffSet;
    }

    //b
    public void SpawnBombTrail(float inBombSpacing, int inNumberOfBombs)
    {
        for (int i = 0; i < inNumberOfBombs; i++)
        {
            //offsets the bomb from the player position by the inBombSpacing and the setOfBombs variable
            Vector3 startPos = transform.position + new Vector3(0, inBombSpacing + setOfBombs, 0);
            GameObject bombs = Instantiate(bombPrefab);
            //draws the bombs in a trail by offsetting the positiion by bomb spacing in y according to the number of bombs in the trail
            bombs.transform.position = startPos + new Vector3(0, i * inBombSpacing, 0);
        }
        setOfBombs += inBombSpacing * inNumberOfBombs;
    }


    //Task 2
    public void SpawnBombOnRandomCorner(float inDistance)
    {
        Vector3 startPos = transform.position;
        int corner = Random.Range(0, 4);
        if (corner < 1)
        {
            GameObject bomb = Instantiate(bombPrefab);
            bomb.transform.position = startPos + new Vector3(-inDistance, inDistance, 0);
        }
        else if (corner < 2)
        {
            GameObject bomb = Instantiate(bombPrefab);
            bomb.transform.position = startPos + new Vector3(inDistance, inDistance, 0);
        }
        else if (corner < 3)
        {
            GameObject bomb = Instantiate(bombPrefab);
            bomb.transform.position = startPos + new Vector3(inDistance, -inDistance, 0);
        }
        else
        {
            GameObject bomb = Instantiate(bombPrefab);
            bomb.transform.position = startPos + new Vector3(-inDistance, -inDistance, 0);
        }
        Debug.Log(corner);
    }

    //Task 3

    public void WarpPlayer(Transform target, float ratio)
    {
        //moving the player towards the enemy
        if (ratio <= 1)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, ratio);
        }
    }

    //Task 4
    public void DetectAsteroids(float inMaxRange, List<Transform> inAsteroids)
    {
        //https://grabthiscode.com/csharp/how-to-get-the-length-list-in-c-unity

        for (int i = 0; i < inAsteroids.Count; i++)
        {
            if (Vector3.Distance(transform.position, inAsteroids[i].position) <= inMaxRange)
            {
                Debug.DrawLine(transform.position,
                    (transform.position + (Vector3.Normalize(inAsteroids[i].position - transform.position) * 2.5f)),
                    Color.purple);
            }
        }
    }

    public void PlayerMovement()
    {
        Vector3 accelerationDirection = Vector3.zero;
        if (Keyboard.current.upArrowKey.isPressed)
        {
            accelerationDirection += Vector3.up;
            currentVelocity += accelerationDirection.normalized * currentAcceleration * Time.deltaTime;
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            accelerationDirection += Vector3.down;
            currentVelocity += accelerationDirection.normalized * currentAcceleration * Time.deltaTime;
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            accelerationDirection += Vector3.left;
            currentVelocity += accelerationDirection.normalized * currentAcceleration * Time.deltaTime;
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            accelerationDirection += Vector3.right;
            currentVelocity += accelerationDirection.normalized * currentAcceleration * Time.deltaTime;
        }

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


        if (!Keyboard.current.upArrowKey.isPressed && currentVelocity.y > 0)
        {
            accelerationDirection += Vector3.down;
            currentVelocity += accelerationDirection.normalized * deceleration * Time.deltaTime;
        }
        if (!Keyboard.current.downArrowKey.isPressed && currentVelocity.y < 0)
        {
            accelerationDirection += Vector3.up;
            currentVelocity += accelerationDirection.normalized * deceleration * Time.deltaTime;
        }
        if (!Keyboard.current.leftArrowKey.isPressed && currentVelocity.x < 0)
        {
            accelerationDirection += Vector3.right;
            currentVelocity += accelerationDirection.normalized * deceleration * Time.deltaTime;
        }
        if (!Keyboard.current.rightArrowKey.isPressed && currentVelocity.x > 0)
        {
            accelerationDirection += Vector3.left;
            currentVelocity += accelerationDirection.normalized * deceleration * Time.deltaTime;
        }
        transform.position += currentVelocity * Time.deltaTime;
    }

    //week 4

    public void EnemyRadar(float radius, int circlePoints)
    {
        //a variable which calculates how far apart the points will be from eachother
        int pointDist = 360 / circlePoints;

        //place points for the lines as long as the point location is below or equal to 360, incrimented by our distance variable
        for (int i = 0; i <= 360; i += pointDist)
        {
            //calculate where the next spot will be, if this one is over 360, put it back to 360/0 (they're at the same position)
            int nextSpot = i + pointDist;
            if (nextSpot > 360)
            {
                nextSpot = 0;
            }

            //calculate the angles / where these points are on the circle
            Vector3 startPoint = new Vector3(Mathf.Cos((i) * Mathf.Deg2Rad), Mathf.Sin((i) * Mathf.Deg2Rad), 0);
            Vector3 nextPoint = new Vector3(Mathf.Cos((nextSpot) * Mathf.Deg2Rad), Mathf.Sin((nextSpot) * Mathf.Deg2Rad), 0);
            //draw out green circle like shape using the points. Moving them over to the player, then away by our radius value
            Debug.DrawLine(startPoint * radius + transform.position, nextPoint * radius + transform.position, Color.green);
            //checking if the enemy is within range of our circle
            if (Vector3.Distance(transform.position, enemyTransform.position) <= radius)
            {
                //if they are, make the circle red instead (idk why this one has brackets, it does the same thing with or without them
                Debug.DrawLine((startPoint * radius) + transform.position, (nextPoint * radius) + transform.position, Color.red);
            }
        }

    }

    public void SpawnPowerups(float radius, int numberOfPowerups)
    {
        //a variable which calculates how far apart the points will be from eachother
        int pointDist = 360 / numberOfPowerups;
              //place points for the lines as long as the point location is below or equal to 360, incrimented by our distance variable
        for (int i = 0; i <= 360; i += pointDist)
         {
            //calculate where the next spot will be, if this one is over 360, put it back to 360/0 (they're at the same position)
            int nextSpot = i + pointDist;
                if (nextSpot > 360)
                {
                    nextSpot = 0;
                }

                //calculate where on the circle/what angle they would be at
                Vector3 spawnPoint = new Vector3(Mathf.Cos((i) * Mathf.Deg2Rad), Mathf.Sin((i) * Mathf.Deg2Rad), 0);
                //create the power up object
                GameObject powerUp = Instantiate(powerUpPrefab);
                //move the power up over to the player, then towards the angle and away by our radius value
                powerUp.transform.position = transform.position + spawnPoint * radius;

          }
        }

}




