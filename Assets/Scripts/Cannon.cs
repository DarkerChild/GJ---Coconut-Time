using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject cannonBallTemplate = null;
    [Space]
    [SerializeField] float cannonFireingForce = 1000f;
    Vector3 cannonBallFiringPosition;

    new ParticleSystem particleSystem;

    new Camera camera;
    public float depthOffset = 10f;


    private void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        cannonBallFiringPosition = particleSystem.transform.position;
        camera = Camera.main;
    }


    private void Update()
    {
        AimCannonAtMouse();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            FireCannonBall();
            PlayParticleFX();
        }
        
    }

    private void FireCannonBall()
    {
        GameObject cannonBall = Instantiate(cannonBallTemplate, transform);
        cannonBall.transform.position = cannonBallFiringPosition;
        CannonBall cannonBallScript = cannonBall.GetComponent<CannonBall>();
        cannonBallScript.cannonBallFiringForce = cannonFireingForce;
        cannonBall.SetActive(true);
        Destroy(cannonBall, 2f);
    }

    private void PlayParticleFX()
    {
        particleSystem.Play();
    }

    private void AimCannonAtMouse() 
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPosition = ray.GetPoint(4.5f);
        Vector3 targetDirection = targetPosition - transform.position;
        targetDirection = -targetDirection.normalized;
        transform.rotation = (Quaternion.LookRotation(targetDirection));
    }
}
