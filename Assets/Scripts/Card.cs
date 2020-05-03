using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class Card : MonoBehaviour
{
    public Difficulty cardDiffictulty;
    public int cardValue;
    public bool isHit = false;

    CardHitController cardHitController;

    private void Start()
    {
        cardHitController = FindObjectOfType<CardHitController>();
    }

    private void OnMouseDown()
    {
        if (cardHitController.areCardHitsAllowed)
        {
            cardHitController.cardHit(gameObject);
        }
    }

}
