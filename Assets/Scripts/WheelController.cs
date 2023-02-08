using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;

    public float acceleration = 15000f;
    public float breakingForce = 300f;
    public float maxTurnAngle = 15f;

    private float currentAcceleration = 0f;
    private float currentBrakeForce = 0f;
    private float currentTurnAngle = 0f;

    private void FixedUpdate()
    {
        currentAcceleration = acceleration * Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space))
        {
            currentBrakeForce = breakingForce;
        }
        else
        {
            currentBrakeForce = 0f;
        }

        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;

        frontRight.brakeTorque = currentBrakeForce;
        frontLeft.brakeTorque = currentBrakeForce;
        backRight.brakeTorque = currentBrakeForce;
        backLeft.brakeTorque = currentBrakeForce;

        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;
    }

    public void setCurrentAcceleration()
    {
        currentAcceleration = 0;
        // frontRight.rpm = 0;
        // frontLeft.rpm = 0;
        frontRight.brakeTorque = Mathf.Infinity;
        frontLeft.brakeTorque = Mathf.Infinity;
        // frontLeft.steerAngle = 0;
        // frontRight.steerAngle = 0;
        // currentBrakeForce = breakingForce;
    }
}
