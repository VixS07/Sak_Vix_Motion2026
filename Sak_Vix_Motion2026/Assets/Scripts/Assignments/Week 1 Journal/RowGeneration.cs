using UnityEngine;
using UnityEngine.InputSystem;

public class RowGeneration : MonoBehaviour
{
    // Number of objects in the row
    public int numberOfObjects; 

    //base points of the square
    public Vector2 point1 = new Vector2(1, 1);
    public Vector2 point2 = new Vector2(1, -1);
    public Vector2 point3 = new Vector2(-1, -1);
    public Vector2 point4 = new Vector2(-1, 1);

    //putting the points into an array so they can be looped through
    public Vector2[] points;
    public float time = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        points = new Vector2[] { point1, point2, point3, point4 };
    }

    // Update is called once per frame
    void Update() { 
    
            
        
    }

    public void drawRow()
    {
        for (int j = 0; j < numberOfObjects; j++)
        {
            for (int i = 0; i < points.Length; i++)
            {
                Vector2 pos = points[i];
                Debug.DrawLine(pos, points[(i + 1) % points.Length], Color.purple, time);
            }
            for (int i = 0; i < points.Length; i++)
            {
                //Move the next square to the right
                points[i].x += 2; 
            }
        }
        for(int i = 0; i < points.Length; i++)
        {
            //Reset the position of the points for the next draw
            points[i].x -= 2 * numberOfObjects; 
        }

    }

    public void changeNumInRow(string num)
    {
        //reference for changing a string into an int
        //https://stackoverflow.com/questions/2344411/how-to-convert-string-to-integer-in-c-sharp
        if (int.TryParse(num, out int newNum))
        {
            numberOfObjects = newNum;
            Debug.Log("Number of objects in the row changed to: " + numberOfObjects);
        }
        else
        {
            Debug.LogError("Invalid input for number of objects: " + num);
        }
    }
}

