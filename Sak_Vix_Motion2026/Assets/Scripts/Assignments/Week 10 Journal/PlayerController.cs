using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 playerInput = new Vector2();
    Rigidbody2D rb;

    public float maxSpeed;
    public float accelerationTime;
    public float decelerationTime;
    float maxAcceleration;
    float maxDeceleration;
    public Vector2 currentVelocity;

    public float groundDist;
    public LayerMask ground;

    FacingDirection current;
    public enum FacingDirection
    {
        left, right
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maxAcceleration = maxSpeed / accelerationTime;
        maxDeceleration = maxSpeed / decelerationTime;
    }

    // Update is called once per frame
    void Update()
    {
        //The input from the player needs to be determined and then passed in the to the MovementUpdate which should
        //manage the actual movement of the character.
        MovementUpdate(playerInput);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        //acceleration
        currentVelocity += playerInput.normalized * maxAcceleration * Time.deltaTime;
        //moving the rigid body
        //https://docs.unity.com/en-us/engine/6000.3/script-reference/unityengine/rigidbody2d/linearvelocity
        rb.linearVelocity = currentVelocity;

        //making sure we dont pass max
        if(currentVelocity.x > maxSpeed)
        {
            currentVelocity.x = maxSpeed;
        }
        if(currentVelocity.x < -maxSpeed)
        {
            currentVelocity.x = -maxSpeed;
        }

        if(playerInput == Vector2.zero)
        {
            currentVelocity -= currentVelocity.normalized * maxDeceleration * Time.deltaTime;
            //making sure we decelerate properly
            if(currentVelocity.x < 0.1f && currentVelocity.x > -0.1f)
            {
                currentVelocity.x = 0f;
            }
        }


    }

    public bool IsWalking()
    {
        //is there no velocity pushing movement
        if(currentVelocity.x == 0)
        {
            return false;
        }
        //there is velocity pushing movement
        else
        {
            return true;
        }
    }
    public bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, groundDist, ground);
    }

    public FacingDirection GetFacingDirection()
    {
        if (playerInput.x < 0)
        {
            current = FacingDirection.left;
            return FacingDirection.left;
        }
        else if (playerInput.x > 0)
        {
            current = FacingDirection.right;
            return FacingDirection.right;
        }
        //theres no player input, return whatever it was last
        else
        {
            return current;
        }
    }

    //code retrieved through last years repository 
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            //find out which way the player is trying to move and store it in a vector2
            playerInput = context.ReadValue<Vector2>();
        }

        //make sure were only moving horizontally
        playerInput.y = 0f;
    }

}
