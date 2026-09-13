using UnityEngine;

public class drawline : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //method for checking to see if the vectors are doing what i want them to do
        Vector2 originPosition = new Vector2(0, 0);
        Vector2 targetPosition = new Vector2(3,-2);
        Debug.DrawLine(originPosition, targetPosition, Color.gray,15f);
        Debug.Log(targetPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
