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

    void Update()
    {
        Vector2 offset = transform.position + Vector3.up;
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            SpawnBombAtOffSet(offset);
        }

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            Vector2 direction = (enemyTransform.position - transform.position);
            WarpDrive(direction);
        }
    }

    void SpawnBombAtOffSet(Vector2 inOffSet)
    {
        GameObject bomb =Instantiate(bombPrefab);
        bomb.transform.position = inOffSet;
    }

    void WarpDrive(Vector2 direction)
    {
        transform.position += Vector3.Normalize(direction);
    }
}
