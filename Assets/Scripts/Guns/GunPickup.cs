using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public GameObject ourPistol;
    public AudioSource gunPickup;
    public GameObject currentObjective;
    public GameObject newObjective;

    void OnTriggerEnter(Collider other) {
        gunPickup.Play();
        ourPistol.SetActive(true);
        currentObjective.SetActive(false);
        newObjective.SetActive(true);

        this.gameObject.SetActive(false);
    }
}
