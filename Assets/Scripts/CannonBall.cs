using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Collider collider = GetComponent<SphereCollider>();
        Card card = other.GetComponent<Card>();
        if (card != null)
        {
            collider.enabled = false;
        }
    }
}
 