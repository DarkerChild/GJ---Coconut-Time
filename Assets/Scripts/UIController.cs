using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    LevelManager levelManager;

    [SerializeField] GameObject openingCanvas;
    [SerializeField] GameObject inGameCanvas;
    [Space]
    [SerializeField] GameObject openingPlayButton;
    [SerializeField] GameObject openingPlayPairsButton;


    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        InitiaSetup();
    }

    private void InitiaSetup()
    {
        openingCanvas.SetActive(true);
        inGameCanvas.SetActive(false);
        openingPlayButton.SetActive(true);
        openingPlayPairsButton.SetActive(false);
    }

    public void OpeningPlayButtonPressed()
    {
        openingPlayButton.SetActive(false);
        openingPlayPairsButton.SetActive(true);
    }

    public void OpeningPlayPairsButtonPressed()
    {
        openingCanvas.SetActive(false);
        levelManager.SetGameState(1);
    }
}

