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

    //gravity implimentation
    public float gravity;
    public float apexHeight;
    public float apexTime;
    public float InitialJumpVelocity;

    bool wasJumpTriggered;
    bool stayOnGround;
    public float terminalSpeed;
    FacingDirection current;
    public enum FacingDirection
    {
        left, right
    }

    // Start is called before the first frame update
    void Start()
    {
        //movement
        rb = GetComponent<Rigidbody2D>();
        maxAcceleration = maxSpeed / accelerationTime;
        maxDeceleration = maxSpeed / decelerationTime;

        //gravity
        gravity = -2 * apexHeight / ((apexTime)*(apexTime));
        InitialJumpVelocity = 2 * apexHeight / apexTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //The input from the player needs to be determined and then passed in the to the MovementUpdate which should
        //manage the actual movement of the character.
        MovementUpdate(playerInput);
        Debug.Log(IsGrounded());
    }

    void Update()
    {
        if (playerInput.y > 0 && IsGrounded())
        {
            stayOnGround = false;
            wasJumpTriggered = true;
        }
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        currentVelocity.y = rb.linearVelocity.y;
        //acceleration
        currentVelocity.x += playerInput.normalized.x * maxAcceleration * Time.deltaTime;


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
            currentVelocity.x -= currentVelocity.normalized.x * maxDeceleration * Time.deltaTime;
            //making sure we decelerate properly
            if(currentVelocity.x < 0.1f && currentVelocity.x > -0.1f)
            {
                currentVelocity.x = 0f;
            }
        }

        if (wasJumpTriggered)
        {
            currentVelocity.y = InitialJumpVelocity;
            wasJumpTriggered = false;
        }

        //apply gravity
        if (!IsGrounded())
        {
            currentVelocity.y += gravity * Time.deltaTime;
        }

        if (currentVelocity.y < terminalSpeed)
        {
            currentVelocity.y = terminalSpeed;
        }

        //moving the rigid body
        //https://docs.unity.com/en-us/engine/6000.3/script-reference/unityengine/rigidbody2d/linearvelocity
        rb.linearVelocity = currentVelocity;
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
        //playerInput.y = 0f;
    }

}
