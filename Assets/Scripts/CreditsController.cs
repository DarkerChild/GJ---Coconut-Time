using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    [SerializeField] GameObject creditsCanvas;
    [SerializeField] LevelManager levelManager = null;

    public void SetIsStarting()
    {
        creditsCanvas.SetActive(true);
    }

    public void BackToMainMenu()
    {
        levelManager.SetGameState(0);
    }

    public void SetIsStopping()
    {
        creditsCanvas.SetActive(false);
    }
}
