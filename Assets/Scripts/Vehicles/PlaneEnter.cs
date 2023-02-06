using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.Vehicles.Cars;


public class PlaneEnter : MonoBehaviour
{
    public GameObject vehicleCam;
    public GameObject thePlayer;
    public GameObject liveVehicle;
    public bool canEnter = false;
    public GameObject exitTrig;
    public GameObject planeHUD;
    

    // Update is called once per frame
    void Update()
    {
        if(canEnter == true) 
        
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                //this.gameObject.GetComponent<BoxCollider>().enabled = false;
                vehicleCam.SetActive(true);
                thePlayer.SetActive(false);
            
                liveVehicle.GetComponent<PlaneController>().enabled = true;
                liveVehicle.GetComponent<AudioSource>().enabled = true;
                canEnter = false;
                thePlayer.transform.parent = this.gameObject.transform; // attach the player to the current object
                StartCoroutine(ExitTrigger());
            }
        }
        
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
           canEnter = true; 
        }
        
    }

    void OnTriggerExit(Collider other) 
    {
        canEnter = false;
    }

    IEnumerator ExitTrigger() 
    {
        yield return new WaitForSeconds(0.5f);
        exitTrig.SetActive(true);
        planeHUD.SetActive(true);
    }
}
