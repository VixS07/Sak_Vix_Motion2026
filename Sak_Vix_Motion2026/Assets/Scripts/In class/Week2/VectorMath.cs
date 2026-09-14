using UnityEngine;
using UnityEngine.InputSystem;

public class VectorMath : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Vector3 upDirection = Vector3.up;

        float magnitudeOfUpDirection = upDirection.magnitude;
        Vector2 normalizedUpDirection = upDirection.normalized;

        //Distance from origin to upDirection
        float distanceToUpDirection = Vector2.Distance(upDirection, Vector2.zero);

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //DrawSquare(currentMousePosition, 5f, Color.purple, 0.5f);
    }

    public static Vector2 GetNormalizedVector(Vector2 vector)
    {
        float sizeOfVector = GetMagnitude(vector);

        //gives us a vector that has a size of 1
        //that has the same direction as before
        Vector2 normalizedVector = new Vector2(vector.x , vector.y) / sizeOfVector;

        return normalizedVector;
    }

    public static float GetMagnitude(Vector2 vector)
    {
        return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
    }


    public static void DrawSquare(Vector2 centerPoint, float size, Color colour, float duration) 
    {
        //VectorMath.DrawSquare();

        //EXAMPLES OF STATIC METHODS THAT WE CAN CALL ANYWHERE:
        //Vector2.Distance();
        //Debug.Log();
        //Mathf.Sqrt()

        //Center point & the size

        //Color

        //Duration how long to show

        //top line
        Vector2 startPoint = centerPoint + new Vector2(-size, size);
        Vector2 endPoint = centerPoint + new Vector2(size, size);

        Debug.DrawLine(startPoint, endPoint, colour, duration);

        //left line
        startPoint = centerPoint + new Vector2(-size, size);
        endPoint = centerPoint + new Vector2(-size, -size);

        Debug.DrawLine(startPoint, endPoint, colour, duration);

        //bottom line
        startPoint = centerPoint + new Vector2(-size, -size);
        endPoint = centerPoint + new Vector2(size, -size);

        Debug.DrawLine(startPoint, endPoint, colour, duration);

        //right line
        startPoint = centerPoint + new Vector2(size, -size);
        endPoint = centerPoint + new Vector2(size, size);

        Debug.DrawLine(startPoint, endPoint, colour, duration);
    }


}
