using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class Card : MonoBehaviour
{
    public GameObject textObject;
    public CardController.Difficulty cardDiffictulty;
    public int cardValue;

    CardController cardController;
    
    private void Start()
    {
        cardController = FindObjectOfType<CardController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        print(gameObject.name + "hit trigger by " + other.name);
        cardController.cardHit(gameObject);
    }

    private void OnMouseDown()
    {
        cardController.cardHit(gameObject);
    }
    
    public void SetCardValue(int newValue)
    {
        cardValue = newValue;
        textObject.GetComponent<TextMesh>().text = newValue.ToString();
    }
}
