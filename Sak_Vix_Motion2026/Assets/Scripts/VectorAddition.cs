using UnityEngine;
using UnityEngine.InputSystem;
public class VectorAddition : MonoBehaviour
{
    public Transform rTransform;
    public Transform bTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //adding the two vectors together
        Vector2 rPlusB = rTransform.position + bTransform.position;
        //drawing the blue line from the origin to the blue vector
        if (Keyboard.current.bKey.isPressed)
        {
            Debug.DrawLine(Vector2.zero, bTransform.position, Color.blue);
        }
        //drawing the red line from the origin to the red vector
        if (Keyboard.current.rKey.isPressed)
        {
            Debug.DrawLine(Vector2.zero, rTransform.position, Color.red);
        }
        //drawing the magenta line from the origin to the sum of the two vectors
        if (Keyboard.current.bKey.isPressed && Keyboard.current.rKey.isPressed)
        {
            Debug.DrawLine(Vector2.zero, rPlusB, Color.magenta);
        }
        //calculating the magnitude of rPlusB and displaying it in the console
        //using pythagorean theorem to calculate the magnitude of the vector
        float magnitudeOfRPlusB = Mathf.Sqrt(rPlusB.x * rPlusB.x + rPlusB.y * rPlusB.y);
        //displaying the number in the console
        Debug.Log(magnitudeOfRPlusB);
        //keeping the magenta line drawn even if the keys are not pressed 
        //to help visualize the sum of the two vectors
        Debug.DrawLine(Vector2.zero, rPlusB, Color.magenta);

        //calculating the direction of the vector by subtracting r from b
        Vector2 fromRToB = bTransform.position - rTransform.position;
    }
}
