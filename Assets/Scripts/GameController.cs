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

    public int currentLevel = 0;
    private int timeRemaining;
    CardSetup CardSetup;

    
    public Difficulty currentDifficulty = Difficulty.Easy;

    private void Start()
    {
        CardSetup = FindObjectOfType<CardSetup>();
    }

    public void StartNewGame()
    {
        isGameActive = false;
        currentLevel = 1;
        timeRemaining = startingTime;
        StopAllCoroutines();
        StartCoroutine(SetGameActive(gameStartDelay));
        CardSetup.StartLevel();
    }

    IEnumerator SetGameActive(float startDelay)
    {
        while (!isGameActive)
        {
            timerText.text = timeRemaining.ToString();
            yield return new WaitForSeconds(startDelay);
            isGameActive = true;
        }
        StartCoroutine(GameTimerControler());
    }

    IEnumerator GameTimerControler()
    {
        while (timeRemaining>=0)
        {
            if (timeRemaining > startingTime) timeRemaining = startingTime;
            timerText.text = timeRemaining.ToString();
            timeRemaining--;
            yield return new WaitForSeconds(1f);
        }
        GameLost();
    }

    private void GameLost()
    {
        isGameActive = false;
        print("Game Lost");
    }

    public void TimeGained(int timeGained)
    {
        timeRemaining += timeGained;
        timerText.text = timeRemaining.ToString();
    }

    public void MoveToNextLevel()
    {
        currentLevel++;
        print("Adding: " + (startingTime - ((currentLevel < startingTime) ? currentLevel : 0)).ToString());
        timeRemaining += startingTime - ((currentLevel<startingTime)? currentLevel:0) ;
    }
}
