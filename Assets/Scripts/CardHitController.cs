using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHitController : MonoBehaviour
{
    [SerializeField] PairsGame pairsGame;
    [SerializeField] CardSetup cardSetup;
    [SerializeField] CardMovementController cardMovementController;
    [Space]
    [SerializeField] public float cardShowTime = 0.5f;
    [SerializeField] int timeGainedForPair = 1;
    [Space]
    [SerializeField] public bool oneCardHit = false; // make private
    [SerializeField] public int firstCardNumber = -1; // make private
    [SerializeField] public GameObject firstCardObject; // make private
    [Space]
    public int secondCardNumber = -1; // make private
    public GameObject secondCardObject; // make private
    public bool areCardHitsAllowed = false;

    public void cardHit(GameObject card)
    {
        if (pairsGame.isGameActive)
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
        firstCardObject = card;
        firstCardNumber = card.GetComponent<Card>().cardValue;
        card.GetComponent<Card>().IsHit(true);
        cardMovementController.RotateCard180(card);
    }

    private void ProcessSecondCardHit(GameObject card)
    {
        if (!card.GetComponent<Card>().isHit)
        {
            areCardHitsAllowed = false;
            secondCardObject = card;
            secondCardNumber = card.GetComponent<Card>().cardValue;
            card.GetComponent<Card>().IsHit(true);

            bool matchingPair = (firstCardNumber == secondCardNumber && firstCardObject != secondCardObject);
            StartCoroutine(PairMatched(matchingPair, firstCardObject, secondCardObject));
            firstCardNumber = -1;
            firstCardObject = null;
            secondCardNumber = -1;
            secondCardObject = null;
            oneCardHit = false;
            areCardHitsAllowed = true;
        }
    }

    IEnumerator PairMatched(bool correct, GameObject card1, GameObject card2)
    {
        cardMovementController.RotateCard180(card2);
        if (correct)
        {
            pairsGame.TimeGained(timeGainedForPair);
        }
        else
        {
            pairsGame.TimeGained(-timeGainedForPair);
        }
        yield return new WaitForSeconds(cardShowTime);

        if (correct)
        {
            card1.SetActive(false); 
            card2.SetActive(false);
        }

        StartCoroutine(ResetCard(card1));
        StartCoroutine(ResetCard(card2));

        if (correct)
        {
            RunFinalPairCheck();
        }
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
        StartCoroutine(pairsGame.MoveToNextLevel());
    }

    IEnumerator ResetCard(GameObject card)
    {

        cardMovementController.RotateCard180(card);
        yield return new WaitForSeconds(cardMovementController.cardSpinTime);
        card.GetComponent<Card>().IsHit(false);
    }

    public void ResetVariables()
    {
        firstCardNumber = -1;
        firstCardObject = null;
        secondCardNumber = -1;
        secondCardObject = null;
        oneCardHit = false;
    }
}
