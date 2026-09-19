using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    void Update()
    {
        
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {

            //adds the bombOffset to the players position
            Vector3 offset = transform.position + bombOffset;
            //passes the offset into the SpawnBombAtOffSet method
            SpawnBombAtOffSet(offset);
        }

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            //calculated the direction from the player to the enemy
            Vector3 direction = (enemyTransform.position - transform.position);
            //passes the direction into the WarpDrive method
            WarpDrive(direction);
        }

        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            SpawnBombTrail(bombTrailSpacing, numberOfTrailBombs);
        }
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


    void WarpDrive(Vector3 direction)
    {
        transform.position += Vector3.Normalize(direction);
    }
}
