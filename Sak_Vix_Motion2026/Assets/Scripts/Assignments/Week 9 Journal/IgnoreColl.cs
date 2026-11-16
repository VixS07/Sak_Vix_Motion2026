using UnityEngine;
using UnityEngine.InputSystem;

public class IgnoreColl : MonoBehaviour
{
    public GameObject moon;
    public bool ignore = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Physics2D.IgnoreCollision(moon.GetComponent<CircleCollider2D>)
        CollisionIgnore(moon.GetComponent<Collider2D>(), GetComponent<Collider2D>(), ignore);
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            ignore = false;
        }
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            ignore = true;
        }
        GetIgnoreCollision(moon.GetComponent<Collider2D>(), GetComponent<Collider2D>());


    }

    public void CollisionIgnore(Collider2D collider1, Collider2D collider2, bool ignore)
    { 
        Physics2D.IgnoreCollision(collider1, collider2, ignore);
        
    }
      
    public static void GetIgnoreCollision(Collider2D collider1, Collider2D collider2)
    {
        bool Ignoring = Physics2D.GetIgnoreCollision(collider1, collider2);
        Debug.Log(Ignoring);
    }

}
