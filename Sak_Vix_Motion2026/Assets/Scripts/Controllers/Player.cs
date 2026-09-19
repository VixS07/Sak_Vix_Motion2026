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
    public Vector3 bombOffset;
    public int numberOfTrailBombs;
    public float bombTrailSpacing;

    void Update()
    {
        Vector3 offset = transform.position + bombOffset;
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            SpawnBombAtOffSet(offset);
        }

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            Vector3 direction = (enemyTransform.position - transform.position);
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
            Vector3 startPos = transform.position + new Vector3 (0, inBombSpacing, 0);
            GameObject bombs = Instantiate(bombPrefab);
            bombs.transform.position = startPos + new Vector3( 0, i * inBombSpacing, 0);
        }
    }


    void WarpDrive(Vector3 direction)
    {
        transform.position += Vector3.Normalize(direction);
    }
}
