using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opening : MonoBehaviour
{
    private bool isStopping = false;
    private bool isStarting = false;

    [SerializeField] GameObject openingCanvas;
    [SerializeField] UIController uiController;
    [SerializeField] CameraController cameraController;

    public void SetIsStopping()
    {
        isStopping = true;
        StartCoroutine(Stopping());
    }

    IEnumerator Stopping()
    {
        openingCanvas.SetActive(false);
        yield return 0;
        isStopping = false;
    }

    public void SetIsStarting()
    {
        openingCanvas.SetActive(true);
        uiController.InitialSetup();
        StartCoroutine(cameraController.OpeningStateCameraAction());
    }

    public bool GetIsStopoping()
    {
        return isStopping;
    }

    public bool GetIsStarting()
    {
        return isStarting;
    }
}
