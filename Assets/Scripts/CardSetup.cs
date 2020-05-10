using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Colours { Sand, BlueComplementary, Red, Gold, BlueSplit, TealSplit, PurpleTriadic, GreenTriadic, PinkTetradic}

public class CardSetup : MonoBehaviour
{
    [SerializeField] PairsGame pairsGame;
    [SerializeField] CardHitController cardHitController;
    [SerializeField] CardMovementController cardMovementController;
    
    [Space]
    [SerializeField] float timeBeforeShowCardsOnNewLevel = 0.5f;
    [SerializeField] float timeToShowCardsOnNewLevel = 0.5f;
    [SerializeField] Dictionary<Colours, Color> colours = new Dictionary<Colours, Color>();

    [Space]
    [SerializeField] Sprite[] timeSprites;

    Card[] allCards;
    List<Card> activeCards = new List<Card>();
    List<Sprite> unusedSprites = new List<Sprite>();

    
    private void Awake()
    {
        SetColours();
        allCards = FindObjectsOfType<Card>();
    }

    public void StartLevel()
    {
        cardHitController.areCardHitsAllowed = false;
        SetActiveCards();
        AssignSprites();
        StartCoroutine(ShowCards());
    }

    private void SetActiveCards()
    {
        activeCards.Clear();
        foreach (Card card in allCards)
        {
            switch (pairsGame.currentDifficulty)
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

    public void AssignSprites()
    {
        Card chosenCard;
        unusedSprites.Clear();
        foreach (Sprite sprite in timeSprites)
        {
            unusedSprites.Add(sprite);
        }
        int i = 0;
        while (activeCards.Count > 0)
        {
            int newValue = Random.Range(0, unusedSprites.Count);
            chosenCard = activeCards[Random.Range(0, activeCards.Count)];  //Pick one of the cards at random
            chosenCard.cardValue = i;
            chosenCard.transform.GetComponentInChildren<SpriteRenderer>().sprite = unusedSprites[newValue];
            activeCards.Remove(chosenCard);

            chosenCard = activeCards[Random.Range(0, activeCards.Count)];  //Pick one of the cards at random
            chosenCard.cardValue = i;
            chosenCard.transform.GetComponentInChildren<SpriteRenderer>().sprite = unusedSprites[newValue];
            activeCards.Remove(chosenCard);

            unusedSprites.RemoveAt(newValue);
            i++;
            
        }
    }

    public void AssignColours()
    {
        Card chosenCard;
        while (activeCards.Count > 0)
        {
            int newValue = Random.Range(0, colours.Count);
            Colours newColorName = (Colours)newValue;
            Color newColor = colours[newColorName];

            chosenCard = activeCards[Random.Range(0, activeCards.Count)];  //Pick one of the cards at random
            chosenCard.cardValue = newValue;
            chosenCard.transform.Find("Front").GetComponent<MeshRenderer>().material.SetColor("_Color",newColor);
            activeCards.Remove(chosenCard);

            chosenCard = activeCards[Random.Range(0, activeCards.Count)];  //Pick one of the cards at random
            chosenCard.transform.Find("Front").GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
            chosenCard.cardValue = newValue;

            activeCards.Remove(chosenCard);
        }
    }


    IEnumerator ShowCards()
    {
        pairsGame.isTimerActive = false;
        yield return new WaitForSeconds(timeBeforeShowCardsOnNewLevel);
        foreach (Card card in allCards)
        {
            cardMovementController.RotateCard180(card.gameObject);
        }
        yield return new WaitForSeconds(timeToShowCardsOnNewLevel);
        foreach (Card card in allCards)
        {
            cardMovementController.RotateCard180(card.gameObject);
        }
        yield return new WaitForSeconds(cardMovementController.cardSpinTime);
        cardHitController.areCardHitsAllowed = true;
        pairsGame.isTimerActive = true;
    }

    private void SetColours()
    {
        colours.Add(Colours.Sand, new Color(0.729f, .46f,.28f));
        colours.Add(Colours.BlueComplementary, new Color(.278f, .545f, .729f));
        colours.Add(Colours.Red, new Color(.729f, .278f, .318f));
        colours.Add(Colours.Gold, new Color(.729f, .69f, .278f));
        colours.Add(Colours.BlueSplit, new Color(.278f, .218f, .729f));
        colours.Add(Colours.TealSplit, new Color(.278f, .729f, .69f));
        colours.Add(Colours.PurpleTriadic, new Color(.463f, .278f, .729f));
        colours.Add(Colours.GreenTriadic, new Color(.278f, .729f, .463f));
        colours.Add(Colours.PinkTetradic, new Color(.729f, .278f, .545f));
    }

    public IEnumerator GameEndCardShowAndReset()
    {
        yield return new WaitForSeconds(timeToShowCardsOnNewLevel);
        //Turn all hit card to face player. (Inactive ones too)
        foreach (Card card in allCards)
        {
            if (card.GetComponent<Card>().isHit == false)
            {
                cardMovementController.RotateCard180(card.gameObject);
            }
        }
        yield return new WaitForSeconds(timeToShowCardsOnNewLevel);
        //Make all cards go away
        foreach (Card card in allCards)
        {
            card.gameObject.SetActive(false);
        }
        //Turn all cards back around away from the player
        foreach (Card card in allCards)
        {
            cardMovementController.RotateCard180(card.gameObject);
            card.GetComponent<Card>().isHit = false;
        }

        cardHitController.ResetVariables();
        pairsGame.PairsGamePreGameSetup();
    }
}
