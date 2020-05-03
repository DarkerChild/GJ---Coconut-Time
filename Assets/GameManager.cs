using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { Opening, InGame, Credits, Tutorial, GameOver };
public class GameManager : MonoBehaviour
{
    public GameStates currentGameState;
    CameraController cameraController;
    OpeningController openingController;
    InGameController inGameController;

    private void Start()
    {
        currentGameState = GameStates.Opening;
        cameraController = FindObjectOfType<CameraController>();
        openingController = FindObjectOfType<OpeningController>();
        inGameController = FindObjectOfType<InGameController>();
    }

    public void SetGameState(int index)
    {
        StopGameState(currentGameState);
        GameStates newGameState = new GameStates();
        switch (index)
        {
            case 0:
                newGameState = GameStates.Opening;
                break;
            case 1:
                newGameState = GameStates.InGame;
                break;
            case 2:
                newGameState = GameStates.Credits;
                break;
            case 3:
                newGameState = GameStates.Tutorial;
                break;
            case 4:
                newGameState = GameStates.GameOver;
                break;
        }
        StartCoroutine(GoToGameState(newGameState));
    }

    IEnumerator GoToGameState(GameStates newGameState)
    {
        currentGameState = newGameState;
        cameraController.GoToGameState(newGameState);
        while (cameraController.GetIsTransitioning())
        {
            yield return 0;
        }
        yield return new WaitForSeconds(0.5f);
        StartGameState();
    }

    private void StopGameState(GameStates oldGameState)
    {
        switch (oldGameState)
        {
            case GameStates.Opening:
                openingController.SetIsStopping();
                break;
            case GameStates.InGame:
                break;
            case GameStates.Credits:
                break;
            case GameStates.Tutorial:
                break;
            case GameStates.GameOver:
                break;
        }
    }

    private void StartGameState()
    {
        switch (currentGameState)
        {
            case GameStates.Opening:
                openingController.SetIsStarting();
                break;
            case GameStates.InGame:
                inGameController.SetIsStarting();
                break;
            case GameStates.Credits:
                break;
            case GameStates.Tutorial:
                break;
            case GameStates.GameOver:
                break;
        }
    }
}
