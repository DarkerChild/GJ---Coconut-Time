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
        Vector3 firingDirection = Quaternion.Euler(transform.rotation.eulerAngles) * Vector3.back;
        rigidbody.AddForce(firingDirection * cannonBallFiringForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = GetComponent<SphereCollider>();
        collider.enabled = false;
        print("Cannon Ball collider disabled");
    }
}
