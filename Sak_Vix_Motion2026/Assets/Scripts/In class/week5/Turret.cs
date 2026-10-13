using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform targetTransform;
    public float rotationSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.up, Color.red);

        Vector3 directionToTarget = targetTransform.position - transform.position;


        //Should we turn left or right:
        bool shouldWeTurnRight = false;
        float dotProductOfRight = TestAngles.VectorDot(directionToTarget, transform.right);
        shouldWeTurnRight = dotProductOfRight > 0f;

        if (shouldWeTurnRight)
        {
            transform.eulerAngles -= Vector3.forward * rotationSpeed * Time.deltaTime;
        }
        else
        {
            transform.eulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;
        }

        Debug.Log(shouldWeTurnRight);
    }
}
