using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Difficulty { Easy, Medium, Hard };
public enum Game { Pairs };

public class PairsGame : MonoBehaviour
{
    [SerializeField] CardSetup cardSetup = null;
    [SerializeField] CardHitController cardHitController = null;
    [SerializeField] LevelManager levelManager = null;
    [SerializeField] GameObject inGameCanvas = null;
    [SerializeField] Cannon cannonScript = null;
    [SerializeField] GameObject planksGroup = null;
    [SerializeField] PractiseTargetController practiseTargetController = null;
    [Space]
    [SerializeField] GameObject timerTextObject = null;
    [SerializeField] Text timerText = null;
    [Space]
    [SerializeField] int startingTime = 20;
    [SerializeField] float gameStartDelay = 0.5f;
    
    public bool isGameActive = false;
    public bool isTimerActive = false;

    public int currentLevel = 0;
    private int timeRemaining;

    public Difficulty currentDifficulty = Difficulty.Easy;

    public void PairsGamePreGameSetup()
    {
        inGameCanvas.SetActive(true);
        SetButtonsActive(true);
        SetPlanksActive(true);
        SetCannonActive(true);
    }

    public void SetButtonsActive(bool isActive)
    {
        Button[] buttons = FindObjectsOfType<Button>();

        foreach (Button button in buttons)
        {
            button.interactable = isActive;
        }
    }

    public void StartNewGameEasy()
    {
        currentDifficulty = Difficulty.Easy;
        StartGameActions();
    }
    public void StartNewGameMedium()
    {
        currentDifficulty = Difficulty.Medium;
        StartGameActions();
    }
    public void StartNewGameHard()
    {
        currentDifficulty = Difficulty.Hard;
        StartGameActions();
    }

    private void StartGameActions()
    {
        SetButtonsActive(false);
        practiseTargetController.DestroyAllTargets();
        StopAllCoroutines();
        timeRemaining = startingTime;
        timerText.text = timeRemaining.ToString();
        isGameActive = true;
        currentLevel = 1;
        SetPlanksActive(false);
        cardSetup.gameObject.SetActive(true);
        timerTextObject.SetActive(true);
        cardSetup.StartLevel();
        StartCoroutine(StartGameTimer());
    }

    IEnumerator StartGameTimer()
    {
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


    private void SetPlanksActive(bool isActive)
    {
        planksGroup.SetActive(isActive);
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

    public void SetCannonActive(bool isActive)
    {
        cannonScript.enabled = isActive;
    }
}
