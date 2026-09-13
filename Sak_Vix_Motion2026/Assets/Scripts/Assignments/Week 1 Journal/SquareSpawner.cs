using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class SquareSpawner : MonoBehaviour
{
    //base points of the square
    public Vector2 point1 = new Vector2(1, 1);
    public Vector2 point2 = new Vector2(1, -1);
    public Vector2 point3 = new Vector2(-1, -1);
    public Vector2 point4 = new Vector2(-1, 1);

    public Vector2[] points;

    public float scale = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //making the vectors into a list 
        points = new Vector2[] { point1, point2, point3, point4 };

    }

    // Update is called once per frame
    void Update()
    {


        //making the square follow the mouse position using a for loop
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        for(int i = 0; i < points.Length; i++)
        {
            Vector2 pos = points[i] + mousePos;
            Debug.DrawLine(pos, points[(i + 1) % points.Length] + mousePos, Color.grey);
        }

        //making the square spawn when the mouse is left clicked
        if (Mouse.current.leftButton.wasPressedThisFrame){
            //using a for loop to draw the square when and where the mouse is clicked
            for (int i = 0; i < points.Length; i++)
            {
                Vector2 pos = points[i] + mousePos;
                Debug.DrawLine(pos, points[(i + 1) % points.Length] + mousePos, Color.white, 5f);
            }

        }

        //scaling the square down using the scroll wheel
        Vector2 scrollValue = Mouse.current.scroll.ReadValue();
        if(scrollValue.y > 0)
        {
            Debug.Log("mousescroll moved up");
            Debug.Log("scroll value: " + scrollValue.y);
        }
        else if (scrollValue.y < 0)
        {
            Debug.Log("mousescroll moved down");
            Debug.Log("scroll value: " + scrollValue.y);
        }
        if(scrollValue.y != 0)
        {
            for (int i = 0; i < points.Length; i++)
            {
                if(i == 0)
                {
                    points[0].y += (scale * scrollValue.y);
                    points[0].x += (scale * scrollValue.y);
                }
            }
        }

    }
}
