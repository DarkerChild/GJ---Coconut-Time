using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairsPractiseTargets : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 startRotation;

    public bool isHit = false;

    PractiseTargetController controller;

    private void Start()
    {
        controller = FindObjectOfType<PractiseTargetController>();
        startPosition = transform.position;
        startRotation = transform.rotation.eulerAngles;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isHit)
        {
            isHit = true;
            controller.TargetHit();
        }
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        transform.rotation = Quaternion.Euler(startRotation);
    }
}
