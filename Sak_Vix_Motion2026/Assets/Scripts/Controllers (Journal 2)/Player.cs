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
    public List<Transform>inAsteroids;

    //week 3
    public Vector3 currentVelocity;
    public float speed;
    public float accelerationTime;
    public float decelerationTime;
    public float currentAcceleration;
    float deceleration;
    public Vector3 maxSpeed = new Vector3(3,3,3);

    void Start()
    {
        currentAcceleration = speed / accelerationTime;
        deceleration = speed / decelerationTime;
    }

    void Update()
    {
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
        if(Keyboard.current.rKey.wasPressedThisFrame)
        {
            SpawnBombOnRandomCorner(distance); 
        }
            //https://stackoverflow.com/questions/66733504/get-a-list-of-transforms-in-unity

            inAsteroids = GameObject.FindGameObjectsWithTag("Asteroid").Select(go => go.transform).ToList();
            DetectAsteroids(inMaxRange, inAsteroids);

        //week 3
        PlayerMovement();

    }
    //Task 1
    //a
    public void SpawnBombAtOffSet(Vector2 inOffSet)
    {
        GameObject bomb =Instantiate(bombPrefab);
        bomb.transform.position = inOffSet;
    }

    //b
    public void SpawnBombTrail(float inBombSpacing, int inNumberOfBombs)
    {
        for(int i = 0; i < inNumberOfBombs; i++)
        {
            //offsets the bomb from the player position by the inBombSpacing and the setOfBombs variable
            Vector3 startPos = transform.position + new Vector3 (0, inBombSpacing + setOfBombs, 0);
            GameObject bombs = Instantiate(bombPrefab);
            //draws the bombs in a trail by offsetting the positiion by bomb spacing in y according to the number of bombs in the trail
            bombs.transform.position = startPos + new Vector3( 0, i * inBombSpacing, 0);
        }
        setOfBombs += inBombSpacing * inNumberOfBombs;
    }


    //Task 2
    public void SpawnBombOnRandomCorner(float inDistance)
    {
        Vector3 startPos = transform.position;
        int corner = Random.Range(0, 4);
        if(corner < 1)
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
        }
        else if (Keyboard.current.downArrowKey.isPressed)
        {
            accelerationDirection += Vector3.down;
        }
        else if (Keyboard.current.leftArrowKey.isPressed)
        {
            accelerationDirection += Vector3.left; 
        }
        else if(Keyboard.current.rightArrowKey.isPressed)
        {
            accelerationDirection += Vector3.right;
        }

        currentVelocity += accelerationDirection.normalized * currentAcceleration * Time.deltaTime;

        //taken from Ryan 
        currentVelocity = Vector3.Min(currentVelocity, maxSpeed);
        currentVelocity = Vector3.Max(currentVelocity, -maxSpeed);

        transform.position += currentVelocity * Time.deltaTime;

    }
}

