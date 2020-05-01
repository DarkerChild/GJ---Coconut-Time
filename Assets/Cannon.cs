using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject cannonBallTemplate = null;
    [Space]
    [SerializeField] float cannonFireingForce = 1000f;

    private Vector3 cannonForcePosition;

    private void FixedUpdate()
    {
        cannonForcePosition = GetComponentInChildren<ParticleSystem>().transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("Firing");
            FireCannon();
        }
    }

    private void FireCannon()
    {
        GameObject cannonBall = Instantiate(cannonBallTemplate, transform);
        CannonBall cannonBallScript = cannonBall.GetComponent<CannonBall>();
        cannonBallScript.cannonBallFiringForce = cannonFireingForce;
        cannonBall.SetActive(true);
        Destroy(cannonBall, 2f);
    }
}
