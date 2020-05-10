using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject cannonBallTemplate = null;
    [Space]
    [SerializeField] public float cannonFireingForce = 1000f;

    new ParticleSystem particleSystem;

    new Camera camera;
    public float depthOffset = 10f;

    private void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        camera = Camera.main;
    }


    private void Update()
    {
        AimCannonAtMouse();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        //if (Input.GetKeyDown(KeyCode.Space))
        {
            FireCannonBall();
            PlayParticleFX();
        }
    }

    private void AimCannonAtMouse()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPosition = ray.GetPoint(4.5f);
        Vector3 targetDirection = targetPosition - transform.position;
        targetDirection = -targetDirection.normalized;
        transform.rotation = (Quaternion.LookRotation(targetDirection));
    }

    private void FireCannonBall()
    {
        GameObject cannonBall = Instantiate(cannonBallTemplate);
        cannonBall.transform.position = particleSystem.transform.position;

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPosition = ray.GetPoint(4.5f) + new Vector3 (0f,0.3f,0f);
        Vector3 targetDirection = targetPosition - cannonBall.transform.position;
        targetDirection = -targetDirection.normalized;
        cannonBall.transform.LookAt(targetPosition);

        Rigidbody rigidbody = cannonBall.GetComponent<Rigidbody>();
        rigidbody.AddForce(-targetDirection * cannonFireingForce);
        Destroy(cannonBall, 2f);
    }

    private void PlayParticleFX()
    {
        //particleSystem.Play();
    }
}
