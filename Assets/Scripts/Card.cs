using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class Card : MonoBehaviour
{
    public GameObject textObject;
    public Difficulty cardDiffictulty;
    public int cardValue;

    //CardSetup cardSetup;
    CardHitController cardHitController;
    GameController gameController;

    public bool isHit = false;

    private Quaternion startingRot;
    private Quaternion hitRot;
    private Quaternion currRot;

    private float cardSpinTime;

    private void Start()
    {
        cardHitController = FindObjectOfType<CardHitController>();
        gameController = FindObjectOfType<GameController>();
        startingRot = transform.rotation;
        float startX = startingRot.eulerAngles.x;
        float startY = startingRot.eulerAngles.y;
        float startZ = startingRot.eulerAngles.z;
        hitRot = Quaternion.Euler(startX, startY + 180f, startZ);
        cardSpinTime = cardHitController.cardSpinTime;
    }

    private void OnMouseDown()
    {
        if (cardHitController.areCardHitsAllowed)
        {
            cardHitController.cardHit(gameObject);
        }
    }
    
    public void SetCardValue(int newValue)
    {
        cardValue = newValue;
        textObject.GetComponent<TextMesh>().text = newValue.ToString();
    }
}
