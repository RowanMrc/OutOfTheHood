using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObj : MonoBehaviour
{
    public float rotateSpeed = 0.5f;

    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);   
    }
}
