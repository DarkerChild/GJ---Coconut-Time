using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    LevelManager levelManager;

    [SerializeField] GameObject openingCanvas;
    [SerializeField] GameObject pairsCanvas;
    [SerializeField] GameObject makingTimeCanvas;
    [Space]
    [SerializeField] GameObject openingPlayButton;
    [SerializeField] GameObject openingCreditsButton;
    [SerializeField] GameObject openingPlayFindingTimeButton;


    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        InitialSetup();
    }

    public void InitialSetup()
    {
        openingCanvas.SetActive(true);
        pairsCanvas.SetActive(false);
        makingTimeCanvas.SetActive(false);
        openingPlayButton.SetActive(true);
        openingCreditsButton.SetActive(true);
    }

    public void OpeningPlayButtonPressed()
    {
        openingCanvas.SetActive(false);
        levelManager.SetGameState(1);
    }

    public void OpeningPlayFindingTimeButtonPressed()
    {
        openingCanvas.SetActive(false);
        levelManager.SetGameState(6);
    }

    public void OpeningCreditsButtonPressed()
    {
        openingCanvas.SetActive(false);
        levelManager.SetGameState(2);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}

