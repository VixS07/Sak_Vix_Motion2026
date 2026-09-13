using UnityEngine;

public class VectorOperations : MonoBehaviour
{
    public Vector2 redVector; //(1,3)
    public Vector2 blueVector;//(2,2)
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 redPlusBlue = redVector + blueVector;
        //R + B : (1,3) + (2,2) = (3,5)
        Vector2 redMinusBlue = redVector - blueVector;
        //R - B : (1,3) - (2,2) = (-1,1)

        Vector2 origin = new Vector2 (0,0);
        //visualizing the base vectors
        Debug.DrawLine(origin, redVector, Color.red);
        Debug.DrawLine(origin, blueVector, Color.blue);
        //visualizing the sum and difference of the vectors
        Debug.DrawLine(origin, redPlusBlue, Color.purple);
        Debug.DrawLine(origin, redMinusBlue, Color.orange);
    }
}
