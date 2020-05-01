using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    new Rigidbody rigidbody;

    public float cannonBallFiringForce = 10f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        print(transform.rotation.eulerAngles);
        Vector3 firingDirection = Quaternion.Euler(transform.rotation.eulerAngles) * Vector3.forward;
        rigidbody.AddForce(firingDirection * cannonBallFiringForce);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
