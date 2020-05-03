using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHitController : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] CardSetup cardSetup;
    [Space]
    [SerializeField] float cardShowTime = 0.5f;
    [SerializeField] public float cardSpinTime = 0.1f;
    [SerializeField] int timeGainedForPair = 1;
    [Space]
    [SerializeField] private bool oneCardHit = false; // make private
    [SerializeField] private int lastCardNumber = -1; // make private
    [SerializeField] GameObject lastCardObject; // make private

    private int currentCardNumber = -1;
    GameObject currentCardObject;
    public bool areCardHitsAllowed = true;

    private Quaternion startingRot;
    private Quaternion hitRot;
    private Quaternion currRot;

    List<Card> activeCards = new List<Card>();

    public void cardHit(GameObject card)
    {
        if (!oneCardHit)
        {
            SetOneCardHit(card);
        }
        else
        {
            areCardHitsAllowed = false;
            currentCardObject = card;
            currentCardNumber = card.GetComponent<Card>().cardValue;

            if (lastCardNumber == currentCardNumber && lastCardObject != currentCardObject)
            {
                StartCoroutine(PairMatchedCorrectly());
                StartCoroutine(RunFinalPairCheck());
                //TODO check if all cards gone
            }
            else
            {
                StartCoroutine(ResetCards());
            }
            //act on card details
        }
    }

    private void SetOneCardHit(GameObject card)
    {
        oneCardHit = true;
        lastCardObject = card;
        card.GetComponent<Card>().isHit = true;
        lastCardNumber = card.GetComponent<Card>().cardValue;
        RotateCard180(card);
    }

    IEnumerator PairMatchedCorrectly()
    {
        RotateCard180(currentCardObject);
        gameController.TimeGained(timeGainedForPair);
        yield return new WaitForSeconds(cardShowTime);
        currentCardObject.SetActive(false);
        lastCardObject.SetActive(false);
        RotateCard180(currentCardObject);
        RotateCard180(lastCardObject);
        oneCardHit = false;
        areCardHitsAllowed = true;
    }

    IEnumerator ResetCards()
    {
        RotateCard180(currentCardObject);

        while (oneCardHit)
        {
            yield return new WaitForSeconds(cardShowTime);

            lastCardNumber = -1;
            RotateCard180(lastCardObject);
            lastCardObject = null;

            currentCardNumber = -1;
            RotateCard180(currentCardObject);
            currentCardObject = null;

            oneCardHit = false;
        }
        areCardHitsAllowed = true;
    }

    IEnumerator RunFinalPairCheck()
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
        if (noOfActiveCards == 2) //Last 2 are deactivated on a delay
        {
            gameController.MoveToNextLevel();
            yield return new WaitForSeconds(cardShowTime * 2);
            cardSetup.StartLevel();
        }
    }

    private void RotateCard180(GameObject card)
    {
        Transform cardTransform = card.transform;
        Quaternion endRot = Quaternion.LookRotation(-cardTransform.forward);
        StartCoroutine(RotateCardSmoothly(card, 180f));
    }

    IEnumerator RotateCardSmoothly(GameObject card, float yRotateAmount)
    {
        float currentRotationAmount = 0;
        float spinAmount = 0;
        while (currentRotationAmount <= yRotateAmount)
        {
            spinAmount = cardSpinTime * Time.deltaTime;
            currentRotationAmount += spinAmount;
            card.transform.Rotate(0f, spinAmount, 0f, Space.Self);
            yield return 0;
        }
        if (currentRotationAmount!= yRotateAmount)
        {
            spinAmount = (yRotateAmount - currentRotationAmount);
            card.transform.Rotate(0f, spinAmount, 0f, Space.Self);
        }
    }
}
