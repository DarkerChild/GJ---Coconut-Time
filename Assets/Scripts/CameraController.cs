using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float openingCameraRotationTime = 10f;
    [SerializeField] float openingCameraFOV = 50f;

    [SerializeField] float InGameCameraFOV = 23.3f;

    [SerializeField] float cameraTransitionTime = 3f;

    [SerializeField] GameObject camera;
    [SerializeField] GameObject openingObject;
    [SerializeField] GameObject gameObject;
    [SerializeField] GameObject creditsObject;
    [SerializeField] LevelManager levelManager;


    [SerializeField] bool isTransitioning = false; //TODO make private

    Dictionary<GameStates, Transform> stateTransforms = new Dictionary<GameStates, Transform>();

    bool startTransitioning = false;
    float openingCameraRotationAmount;



    void Start()
    {
        camera.transform.position = openingObject.transform.position;
        camera.transform.rotation = openingObject.transform.rotation;
        camera.GetComponent<Camera>().fieldOfView = openingObject.transform.localScale.z;
        PopulateTransformDictionary();
        SetInitialValues();
        StartCoroutine(OpeningStateCameraAction());
    }

    private void PopulateTransformDictionary()
    {
        stateTransforms.Add(GameStates.Opening, openingObject.transform);
        stateTransforms.Add(GameStates.Pairs, gameObject.transform);
        stateTransforms.Add(GameStates.Credits, creditsObject.transform);
    }

    private void SetInitialValues()
    {
        openingCameraRotationAmount = (360f / openingCameraRotationTime * Time.deltaTime);
    }

    public IEnumerator OpeningStateCameraAction()
    {
        while (levelManager.currentGameState == GameStates.Opening)
        {
            camera.transform.RotateAround(new Vector3(0f, 0f, 0f), new Vector3(0f, -1f, 0f), openingCameraRotationAmount);
            yield return 0;
        }
    }
    
    public IEnumerator TransitionToNewState(GameStates newGameState)
    {
        isTransitioning = true;
        Transform targetTransform = stateTransforms[newGameState];
        Vector3 targetPosition = targetTransform.position;
        Quaternion targetRotation = targetTransform.rotation;
        float targetFOV = targetTransform.localScale.z;
        Vector3 currentPosition = camera.transform.position;
        Quaternion currentRotation = camera.transform.rotation;
        float currentFOV = camera.GetComponent<Camera>().fieldOfView;
        float FOVDiff = targetFOV - currentFOV;
        float howCloseToTarget = 0f;
        while (howCloseToTarget!=1)
        {
            howCloseToTarget += Time.deltaTime / cameraTransitionTime;
            if (howCloseToTarget > 1) howCloseToTarget = 1f;
            camera.transform.position = Vector3.Lerp(currentPosition, targetPosition, howCloseToTarget);
            camera.transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, howCloseToTarget);
            camera.GetComponent<Camera>().fieldOfView = currentFOV + (howCloseToTarget * FOVDiff);
            yield return 0;
        }        
        isTransitioning = false;
    }

    public bool GetIsTransitioning()
    {
        return isTransitioning;
    }

}
