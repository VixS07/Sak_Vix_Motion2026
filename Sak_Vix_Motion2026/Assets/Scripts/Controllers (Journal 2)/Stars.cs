using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;
    public Vector2 starPos;
    public int i;
    public Vector2 movementDirection;

    void Start()
    {
        starPos = starTransforms[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        DrawConstellation();

    }

    void DrawConstellation()
    {
        Vector2 movementDirection = (starTransforms[i].position - (Vector3)starPos).normalized * drawingTime * Time.deltaTime;
        starPos += movementDirection;

        if (Vector2.Distance(starPos, starTransforms[i].position) <= 0.1f)
        {
            i++;
            if (i == starTransforms.Count)
            {
                i = 0;
            }
        }

        Debug.DrawLine(starPos, starTransforms[i - 1].position, Color.white);
    }
}

