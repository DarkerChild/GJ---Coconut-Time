using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSetup : MonoBehaviour
{
    Card[] allCards;
    List<Card> activeCards = new List<Card>();

    GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        allCards = FindObjectsOfType<Card>();
    }

    public void StartLevel()
    {
        SetupCards();
        AssignValues();
    }

    private void SetupCards()
    {
        activeCards.Clear();
        foreach (Card card in allCards)
        {
            switch (gameController.currentDifficulty)
            {
                case Difficulty.Easy:
                    card.gameObject.SetActive(card.cardDiffictulty == Difficulty.Easy);
                    break;
                case Difficulty.Medium:
                    card.gameObject.SetActive(card.cardDiffictulty != Difficulty.Hard);
                    break;
                case Difficulty.Hard:
                    card.gameObject.SetActive(true);
                    break;
            }
            if (card.gameObject.activeInHierarchy) { activeCards.Add(card); }
        }
    }

    public void AssignValues()
    {
        Card chosenCard;
        while (activeCards.Count > 0)
        {
            int newValue = Random.Range(1, 10);
            chosenCard = activeCards[Random.Range(0, activeCards.Count)];  //Pick one of the cards at random
            chosenCard.SetCardValue(newValue);
            activeCards.Remove(chosenCard);
            chosenCard = activeCards[Random.Range(0, activeCards.Count)];  //Pick one of the cards at random
            chosenCard.SetCardValue(newValue);
            activeCards.Remove(chosenCard);
        }
    }
}
