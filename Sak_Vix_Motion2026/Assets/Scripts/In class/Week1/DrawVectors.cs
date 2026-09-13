using UnityEngine;

public class DrawVectors : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 origin = new Vector2(0, 0);
        Vector2 dVector = new Vector2 (0, 1);
        Vector2 eVector = new Vector2 (3,-2);

        Debug.DrawLine(origin, dVector, Color.yellow, 15f);
        Debug.DrawLine(origin, eVector, Color.grey, 15f);

    }
}
