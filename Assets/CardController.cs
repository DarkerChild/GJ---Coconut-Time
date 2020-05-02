using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField] private bool oneCardHit = false;
    [SerializeField] private int lastCardNumber = -1;
     private int currentCardNumber = -1;

    [SerializeField] GameObject lastCardObject;
    GameObject currentCardObject;

    public enum Difficulty { Easy, Medium, Hard};
    Difficulty currentDifficulty = Difficulty.Easy;

    Card[] allCards;
    List<Card> activeCards = new List<Card>();


    private void Start()
    {
        allCards = FindObjectsOfType<Card>();
    }

    public void StartGame()
    {
        switch (Random.Range(0,3)) {
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
        SetupCards();
        AssignValues();
    }

    private void SetupCards()
    {
        activeCards.Clear();
        foreach (Card card in allCards)
        {
            switch (currentDifficulty)
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
            int newValue = Random.Range(1, 6);
            chosenCard = activeCards[Random.Range(0, activeCards.Count)];  //Pick one of the cards at random
            chosenCard.SetCardValue(newValue);
            activeCards.Remove(chosenCard);
            chosenCard = activeCards[Random.Range(0, activeCards.Count)];  //Pick one of the cards at random
            chosenCard.SetCardValue(newValue);
            activeCards.Remove(chosenCard);
        }
    }

    public void cardHit(GameObject card)
    {
        if (!oneCardHit)
        {
            SetOneCardHit(card);
        }
        else
        {
            currentCardObject = card;
            currentCardNumber = card.GetComponent<Card>().cardValue;

            if (lastCardNumber==currentCardNumber && lastCardObject!=currentCardObject)
            {
                currentCardObject.SetActive(false);
                lastCardObject.SetActive(false);
                ResetCards();

                //TODO check if all cards gone
            }
            else if (lastCardObject != currentCardObject)
            {
                ResetCards();
            }
            //act on card details
        }
    }

    private void SetOneCardHit(GameObject card)
    {
        oneCardHit = true;
        lastCardObject = card;
        lastCardNumber = card.GetComponent<Card>().cardValue;
    }

    private void ResetCards()
    {
        oneCardHit = false;
        lastCardNumber = -1;
        currentCardNumber = -1;
        lastCardObject = null;
        currentCardObject = null;
    }
}
