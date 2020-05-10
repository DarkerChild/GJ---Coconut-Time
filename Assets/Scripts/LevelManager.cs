using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { Opening, Transitioning ,Pairs, MakingTime,Credits, Tutorial, GameOver };
public class LevelManager : MonoBehaviour
{
    public GameStates currentGameState;
    CameraController cameraController;
    Opening openingController;
    PairsGame pairsGame;
    MakingTime makingTime;

    private void Start()
    {
        currentGameState = GameStates.Opening;
        cameraController = FindObjectOfType<CameraController>();
        openingController = FindObjectOfType<Opening>();
        pairsGame = FindObjectOfType<PairsGame>();
        makingTime = FindObjectOfType<MakingTime>();
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
                newGameState = GameStates.Pairs;
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
            case 5:
                newGameState = GameStates.Transitioning;
                break;
            case 6:
                newGameState = GameStates.MakingTime;
                break;
        }
        StartCoroutine(CameraMoveToNewGameState(newGameState));
    }

    IEnumerator CameraMoveToNewGameState(GameStates newGameState)
    {
        currentGameState = newGameState;
        StartCoroutine(cameraController.TransitionToNewState(newGameState));
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
            case GameStates.Pairs:
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
            case GameStates.Pairs:
                pairsGame.PairsGamePreGameSetup();
                break;
            case GameStates.MakingTime:
                makingTime.StartGame();
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
