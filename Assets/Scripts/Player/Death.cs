using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject npcObject;

    void OnTriggerEnter(Collider other)
    {
        npcObject.GetComponent<Animator>().SetTrigger("TrDying");
        npcObject.GetComponent<AudioSource>().Play();
    }
}
