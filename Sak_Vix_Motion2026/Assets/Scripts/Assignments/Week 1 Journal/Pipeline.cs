using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pipeline : MonoBehaviour
{
    public Vector2 lastMousePos;
    public float t = 0;
    bool reset;
    float magnitudeOfLine;
    float totalLengthOfLine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        reset = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if(Mouse.current.leftButton.isPressed)
        {
            if(reset)
            {
                lastMousePos = mousePos;
            }
            t += Time.deltaTime;
            if(t > 0.1f)
            {
                //drawing the line between the last mouse position and the current mouse position
                Debug.DrawLine(lastMousePos, mousePos, Color.green, 100f);
                //using pythagorean theorem to calculate the magnitude of the vector
                Vector2 mouseDistance = lastMousePos + mousePos;
                magnitudeOfLine = Mathf.Sqrt(mouseDistance.x * mouseDistance.x + mouseDistance.y * mouseDistance.y);
                totalLengthOfLine += magnitudeOfLine;
                //resetting the timer and updating the last mouse position
                t = 0;
                lastMousePos = mousePos;
            }
        }
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            reset = true;
        }
        else
        {
            reset = false;
        }

        if(Mouse.current.leftButton.wasReleasedThisFrame)
        {

            Debug.Log(totalLengthOfLine);
            totalLengthOfLine = 0;
        }

    }
}
