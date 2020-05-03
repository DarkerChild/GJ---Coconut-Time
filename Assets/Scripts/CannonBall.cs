using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    new Rigidbody rigidbody;
    Cannon cannon;

    void Start()
    {
        transform.localScale = new Vector3(.1f,.1f,0.1f);
        cannon = FindObjectOfType<Cannon>();
        rigidbody = GetComponent<Rigidbody>();
        Vector3 firingDirection = Quaternion.Euler(transform.rotation.eulerAngles) * Vector3.back;
        rigidbody.AddForce(firingDirection * cannon.cannonFireingForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = GetComponent<SphereCollider>();
        //collider.enabled = false;
    }
}
