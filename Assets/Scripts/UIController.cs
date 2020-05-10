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
    [SerializeField] GameObject openingPlayPairsButton;
    [SerializeField] GameObject openingPlayFindingTimeButton;


    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        InitiaSetup();
    }

    private void InitiaSetup()
    {
        openingCanvas.SetActive(true);
        pairsCanvas.SetActive(false);
        makingTimeCanvas.SetActive(false);
        openingPlayButton.SetActive(true);
        openingPlayPairsButton.SetActive(false);
    }

    public void OpeningPlayButtonPressed()
    {
        openingPlayButton.SetActive(false);
        openingPlayPairsButton.SetActive(true);
        openingPlayFindingTimeButton.SetActive(true);
    }

    public void OpeningPlayPairsButtonPressed()
    {
        openingCanvas.SetActive(false);
        levelManager.SetGameState(1);
    }

    public void OpeningPlayFindingTimeButtonPressed()
    {
        openingCanvas.SetActive(false);
        levelManager.SetGameState(6);
    }
}

