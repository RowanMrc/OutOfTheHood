using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour { 

    private Rigidbody bulletRigidBody;
    Vector3 screenCenter;

    void Awake()
{
    bulletRigidBody = GetComponent<Rigidbody>();
}
   
    void Start()
    {
        float speed = 60f;
        bulletRigidBody.velocity = transform.forward = transform.forward * speed;
    
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Destroy(gameObject);
    }
}
