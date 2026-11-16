using UnityEngine;

public class Linecast : MonoBehaviour
{
    public GameObject start;
    public GameObject end;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //decided to use infinity since I dont want any Z coord limitations as of right now
        //https://docs.unity.com/en-us/engine/6000.3/script-reference/unityengiane/mathf/infinity
        //it tracks layers, not order in layers, so I got the syntax for the default layer
        //https://docs.unity.com/en-us/engine/6000.0/script-reference/unityengine/physics/defaultraycastlayers
        LinecastTest(start.transform.position, end.transform.position, Physics2D.DefaultRaycastLayers , -Mathf.Infinity, Mathf.Infinity);
        
    }

    //layer mask will ensure it only collides with objects on the same layer
    //min and max depth determine the Z coordinate range for the collision
    public static RaycastHit2D LinecastTest(Vector2 start, Vector2 end, int layerMask, float minDepth, float maxDepth)
    {
        RaycastHit2D cast = Physics2D.Linecast(start, end, layerMask, minDepth, maxDepth);

        if (cast !=false)
        {
            Debug.Log("hit");
        }

        return cast;

    }
}
