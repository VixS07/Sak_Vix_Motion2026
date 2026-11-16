using UnityEngine;

public class TorqueTest : MonoBehaviour
{
    public float change;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //AddTorqueImpulse(change);
        AddTorqueImp(change);
        //AddTorqueForce(change);
    }
    //The first is me using the code snippit from the website to get a starter understanding
    //https://docs.unity.com/en-us/engine/6000.3/script-reference/unityengine/rigidbody2d/addtorque
    // Add an impulse which produces a change in angular velocity (specified in degrees).
    public void AddTorqueImpulse(float angularChangeInDegrees)
    {
        var body = GetComponent<Rigidbody2D>();
        var impulse = (angularChangeInDegrees * Mathf.Deg2Rad) * body.inertia;

        body.AddTorque(impulse, ForceMode2D.Impulse);
    }

    //all of the following is my own code
    //impulse
    public void AddTorqueImp(float changeAmount)
    {
        var body = GetComponent<Rigidbody2D>();
        body.AddTorque(changeAmount * Time.deltaTime, ForceMode2D.Impulse);
    }
    //force
    public void AddTorqueForce(float changeAmount)
    {
        var body = GetComponent<Rigidbody2D>();
        body.AddTorque(changeAmount * Time.deltaTime, ForceMode2D.Force);
    }
}
