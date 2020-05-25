using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType { wine, glass1, glass2, glass3 }
public class PairsPractiseTargets : MonoBehaviour
{
    [SerializeField] public TargetType type;
    Vector3 startPosition;
    Vector3 startRotation;

    public bool isHit = false;

    PractiseTargetController controller;

    private void Start()
    {
        FindController();
        startPosition = transform.position;
        startRotation = transform.rotation.eulerAngles;
    }

    private void FindController()
    {
        controller = FindObjectOfType<PractiseTargetController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isHit)
        {
            CannonBall cannonBall = collision.gameObject.GetComponent<CannonBall>();
            PairsPractiseTargets otherTarget = collision.gameObject.GetComponent<PairsPractiseTargets>();
            if (cannonBall != null || otherTarget!=null)
            {
                isHit = true;
                if (controller != null)
                {
                    controller.TargetHit();
                }
                else
                {
                    FindController();
                }
            }
        }
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        transform.rotation = Quaternion.Euler(startRotation);
    }
}
