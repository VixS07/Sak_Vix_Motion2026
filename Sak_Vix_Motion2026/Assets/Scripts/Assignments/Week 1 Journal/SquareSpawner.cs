using UnityEngine;
using UnityEngine.InputSystem;

public class SquareSpawner : MonoBehaviour
{
    //base points of the square
    public Vector2 point1 = new Vector2(1, 1);
    public Vector2 point2 = new Vector2(1, -1);
    public Vector2 point3 = new Vector2(-1, -1);
    public Vector2 point4 = new Vector2(-1, 1);

    //putting the points into an array so they can be looped through
    public Vector2[] points;

    //scale of the square when the mouse scroll wheel is used
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
            //adds the mouse position to the points of the square so it follows the mouse
            Vector2 pos = points[i] + mousePos;
            //draws the square in grey
            //dividing by points.length to make sure the last point connects to the first point
            Debug.DrawLine(pos, points[(i + 1) % points.Length] + mousePos, Color.grey);
        }

        //making the square spawn when the mouse is left clicked
        if (Mouse.current.leftButton.wasPressedThisFrame){
            //using a for loop to draw the square when and where the mouse is clicked
            for (int i = 0; i < points.Length; i++)
            {
                Vector2 pos = points[i] + mousePos;
                //drawing it in white to differenciate this square for the one that follows the mouse
                Debug.DrawLine(pos, points[(i + 1) % points.Length] + mousePos, Color.white, 1000f);
            }
        }

        //scaling the square down using the scroll wheel
        Vector2 scrollValue = Mouse.current.scroll.ReadValue();
        //checking to see if the scroll wheel is being used to help with debugging
        if (scrollValue.y > 0)
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
            //looping through the list
            for (int i = 0; i < points.Length; i++)
            {
                //changing the addition/subtraction of the points based on the quadrant the point is in
                //to make sure the square scales down correctly
                if (i == 0)
                {
                    points[0].y += (scale * scrollValue.y);
                    points[0].x += (scale * scrollValue.y);
                }
            }
        }
    }
}
