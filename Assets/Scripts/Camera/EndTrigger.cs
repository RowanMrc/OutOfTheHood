using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public GameObject canvas;
    public GameObject plane;


    void OnTriggerEnter(Collider other)
    {
        plane.GetComponent<AudioSource>().Stop();
        canvas.SetActive(true);

    }
}
