using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [Header("Plane Stats")]
    [Tooltip("How much the throttle increment or decrement.")]
    public float throttleIncrement = 0.1f;
    [Tooltip("Maximum engine thrust ")]
    public float maxThrust = 200f;
    [Tooltip("How responsive is the plane .")]
    public float responsiveness = 10f;
    [Tooltip("How much lift force needed .")]
    public float lift = 135f;

    private float throttle;
    private float roll; 
    private float pitch;
    private float yaw;  

    private float responseModifier {
    get {
        return (rb.mass / 10f) * responsiveness;
    }
}

Rigidbody rb;
AudioSource engineSound;

private void Awake () 
{
    rb = GetComponent<Rigidbody>();
    engineSound = GetComponent<AudioSource>();
}

private void handleInputs()
{
    roll = Input.GetAxis("Roll");
    pitch = Input.GetAxis("Pitch");
    yaw = Input.GetAxis("Yaw");

    if(Input.GetKey(KeyCode.Space)) throttle += throttleIncrement;
    else if (Input.GetKey(KeyCode.LeftControl)) throttle -= throttleIncrement;
    throttle = Mathf.Clamp(throttle, 0f, 100f);
}

private void Update() {
    handleInputs();
    engineSound.volume = throttle * 0.01f;
}

private void FixedUpdate() {
    //Apply forces to our planes
    rb.AddForce(transform.forward * maxThrust * throttle);
    rb.AddTorque(transform.up * yaw * responseModifier);
    rb.AddTorque(transform.right * pitch * responseModifier * 10);
    rb.AddTorque(transform.forward * roll * responseModifier );

    rb.AddForce(Vector3.up * rb.velocity.magnitude * lift);
}


}
