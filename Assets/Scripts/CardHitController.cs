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

            bool matchingpair = (firstCardNumber == secondCardNumber && firstCardObject != secondCardObject);
            StartCoroutine(PairMatched(matchingpair));
        }
    }

    IEnumerator PairMatched(bool correct)
    {
        cardMovementController.RotateCard180(secondCardObject);
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
            secondCardObject.SetActive(false);
            firstCardObject.SetActive(false);
        }

        oneCardHit = false;
        areCardHitsAllowed = true;
        ResetCard(firstCardObject);
        firstCardNumber = -1;
        firstCardObject = null;

        ResetCard(secondCardObject);
        secondCardNumber = -1;
        secondCardObject = null;

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

    public void ResetCard(GameObject card)
    {
        card.GetComponent<Card>().IsHit(false);
        cardMovementController.RotateCard180(card);
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
