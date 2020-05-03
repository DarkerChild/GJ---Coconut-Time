using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Difficulty { Easy, Medium, Hard };

public class GameController : MonoBehaviour
{
    
    [SerializeField] Text timerText;
    [Space]
    [SerializeField] int startingTime;
    [SerializeField] float gameStartDelay;
    
    public bool isGameActive = false;
    public bool isTimerActive = false;

    public int currentLevel = 0;
    private int timeRemaining;
    CardSetup cardSetup;
    CardHitController cardHitController;

    public Difficulty currentDifficulty = Difficulty.Easy;

    private void Start()
    {
        cardSetup = FindObjectOfType<CardSetup>();
        cardHitController = FindObjectOfType<CardHitController>();
    }

    public void StartNewGame(int difficulty)
    {
        switch (difficulty)
        {
            case 0:
                currentDifficulty = Difficulty.Easy;
                break;
            case 1:
                currentDifficulty = Difficulty.Medium;
                break;
            case 2:
                currentDifficulty = Difficulty.Hard;
                break;
        }
        SetButtonsActive(false);
        StopAllCoroutines();
        StartCoroutine(SetGameActive());
        cardSetup.StartLevel();
    }

    IEnumerator SetGameActive()
    {
        isGameActive = true;
        currentLevel = 1;
        timeRemaining = startingTime;
        timerText.text = timeRemaining.ToString();
        yield return new WaitForSeconds(gameStartDelay);
        
        while (isGameActive)
        {
            while (isTimerActive)
            {
                if (timeRemaining > startingTime) timeRemaining = startingTime;
                if (isGameActive)
                {
                    TimeGained(-1);
                }
                yield return new WaitForSeconds(1f);
            }
            while (!isTimerActive) yield return new WaitForSeconds(1f);
        }
    }



    public void TimeGained(int timeGained)
    {
        int newTimeRemaining = timeRemaining + timeGained;
        if (newTimeRemaining <= 0)
        {
            timeRemaining = 0;
            GameLost();
        }
        else if (newTimeRemaining < startingTime)
        {
            timeRemaining += timeGained;
            
        }
        else
        {
            timeRemaining = startingTime;
        }
        timerText.text = timeRemaining.ToString();
    }

    private void GameLost()
    {
        isGameActive = false;
        StartCoroutine(cardSetup.GameEndCardShowAndReset());
    }

    public IEnumerator MoveToNextLevel()
    {
        currentLevel++;
        int newTimeGained = currentLevel < startingTime ? (startingTime - currentLevel) : 0;
        if (newTimeGained > 0 ) TimeGained(newTimeGained);
        isTimerActive = false;
        yield return new WaitForSeconds(cardHitController.cardShowTime * 2);
        cardSetup.StartLevel();
    }

    public void SetButtonsActive(bool isActive)
    {
        Button[] buttons = FindObjectsOfType<Button>();

        foreach (Button button in buttons)
        {
            button.interactable = isActive;
        }
    }
}
