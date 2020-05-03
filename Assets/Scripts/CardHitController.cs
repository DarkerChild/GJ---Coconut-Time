using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHitController : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] CardSetup cardSetup;
    [SerializeField] CardMovementController cardMovementController;
    [Space]
    [SerializeField] public float cardShowTime = 0.5f;
    [SerializeField] int timeGainedForPair = 1;
    [Space]
    [SerializeField] public bool oneCardHit = false; // make private
    [SerializeField] private int lastCardNumber = -1; // make private
    [SerializeField] GameObject lastCardObject; // make private

    private int currentCardNumber = -1;
    GameObject currentCardObject;
    public bool areCardHitsAllowed = false;

    public void cardHit(GameObject card)
    {
        if (gameController.isGameActive)
        {
            if (oneCardHit)
            {
                ProcessSecondCardHit(card);
            }
            else
            {
                SetOneCardHit(card);
            }
        }
    }

    private void SetOneCardHit(GameObject card)
    {
        oneCardHit = true;
        lastCardObject = card;
        lastCardNumber = card.GetComponent<Card>().cardValue;
        cardMovementController.RotateCard180(card);
    }

    private void ProcessSecondCardHit(GameObject card)
    {
        areCardHitsAllowed = false;
        currentCardObject = card;
        currentCardNumber = card.GetComponent<Card>().cardValue;

        if (lastCardNumber == currentCardNumber && lastCardObject != currentCardObject)
        {
            StartCoroutine(PairMatchedCorrectly());
        }
        else
        {
            gameController.TimeGained(-1);
            StartCoroutine(ResetCards());
        }
    }

    IEnumerator PairMatchedCorrectly()
    {
        cardMovementController.RotateCard180(currentCardObject);
        gameController.TimeGained(timeGainedForPair);
        yield return new WaitForSeconds(cardShowTime);
        currentCardObject.SetActive(false);
        lastCardObject.SetActive(false);
        cardMovementController.RotateCard180(currentCardObject);
        cardMovementController.RotateCard180(lastCardObject);
        oneCardHit = false;
        areCardHitsAllowed = true;
        lastCardNumber = -1;
        lastCardObject = null;
        RunFinalPairCheck();
    }

    IEnumerator ResetCards()
    {
        cardMovementController.RotateCard180(currentCardObject);

        yield return new WaitForSeconds(cardShowTime);

        lastCardNumber = -1;
        cardMovementController.RotateCard180(lastCardObject);
        lastCardObject = null;

        currentCardNumber = -1;
        cardMovementController.RotateCard180(currentCardObject);
        currentCardObject = null;

        oneCardHit = false;
        areCardHitsAllowed = true;
    }

    private void RunFinalPairCheck()
    {
        int noOfActiveCards = 0;
        Card[] allCards = FindObjectsOfType<Card>();
        foreach (Card card in allCards)
        {
            if (card.isActiveAndEnabled)
            {
                noOfActiveCards++;
            }
        }
        if (noOfActiveCards == 0) //Last 2 are deactivated on a delay
        {
            MoveToNextLevel();
        }
    }


    public void MoveToNextLevel()
    {
        areCardHitsAllowed = false;
        ResetOneCard();
        StartCoroutine(gameController.MoveToNextLevel());
    }

    public void ResetOneCard()
    {
        if (oneCardHit)
        {
            lastCardNumber = -1;
            cardMovementController.RotateCard180(lastCardObject);
            lastCardObject = null;
            oneCardHit = false;
        }
    }
}
