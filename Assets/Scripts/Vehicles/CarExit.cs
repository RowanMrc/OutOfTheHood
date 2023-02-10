using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarExit : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;
    public GameObject vehicleCam;
    //  public GameObject ant;
    public GameObject thePlayer;
    public GameObject liveVehicle;
    public GameObject entryTrig;
    private WheelController wc;
    private void Start()
    {
        wc = liveVehicle.GetComponent<WheelController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            wc.setCurrentAcceleration();
            thePlayer.SetActive(true);
            vehicleCam.SetActive(false);
            // frontRight.motorTorque = 0;
            // frontLeft.motorTorque = 0;
            // // frontLeft.steerAngle = 0;
            // // frontRight.steerAngle = 0;

            liveVehicle.GetComponent<WheelController>().enabled = false;
            thePlayer.transform.parent = null;
            StartCoroutine(EnterAgain());
        }
    }


    IEnumerator EnterAgain()
    {
        yield return new WaitForSeconds(0.5f);
        entryTrig.GetComponent<BoxCollider>().enabled = true;
        this.gameObject.SetActive(false);
    }
}
