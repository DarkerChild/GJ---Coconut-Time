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
        if (Input.GetKeyDown(KeyCode.Space))
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
        /*
        Vector3 mousePositionWorldSpace = camera.ScreenToWorldPoint(Input.mousePosition + -camera.transform.forward);
        Vector3 pointOfInterest = mousePositionWorldSpace + camera.transform.forward * depthOffset;
        Vector3 direction = (pointOfInterest - transform.position)*-1f;
        transform.rotation = Quaternion.LookRotation(direction);
        
        Vector3 mousePos = Input.mousePosition;
        mousePos.z += depthOffset;
        Vector3 direction = camera.ScreenToWorldPoint(mousePos);
        transform.rotation = Quaternion.LookRotation(-direction);
        */

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPosition = ray.GetPoint(4.5f);
        Debug.DrawRay(ray.origin, ray.direction * 4.5f, Color.yellow);
        //print("TP : " + targetPosition);
        transform.rotation = Quaternion.LookRotation(targetPosition);
    }
}

//4.417 , 1.315, 4.527
