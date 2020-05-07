using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opening : MonoBehaviour
{
    private bool isStopping = false;
    private bool isStarting = false;

    [SerializeField] GameObject openingCanvas;

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
