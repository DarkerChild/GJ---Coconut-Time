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

    public void StartNewGame()
    {
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

    private void GameLost()
    {
        isGameActive = false;
        cardSetup.ResetCards();
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

    public IEnumerator MoveToNextLevel()
    {
        currentLevel++;
        int newTimeGained = currentLevel < startingTime ? (startingTime - currentLevel) : 0;
        if (newTimeGained > 0 ) TimeGained(newTimeGained);
        isTimerActive = false;
        yield return new WaitForSeconds(cardHitController.cardShowTime * 2);
        cardSetup.StartLevel();
    }
}
