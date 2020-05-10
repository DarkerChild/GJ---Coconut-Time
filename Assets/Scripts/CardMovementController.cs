using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMovementController : MonoBehaviour
{
    [SerializeField] public float cardSpinTime = 1f;

    public void RotateCard180(GameObject card)
    {
        Transform cardTransform = card.transform;
        StartCoroutine(RotateCardSmoothly(card, 180f));
    }

    IEnumerator RotateCardSmoothly(GameObject card, float yRotateAmount)
    {
        float currentRotationAmount = 0;
        float spinAmount = 0;
        while (currentRotationAmount <= yRotateAmount)
        {
            spinAmount = 180f / cardSpinTime * Time.deltaTime;
            currentRotationAmount += spinAmount;
            card.transform.Rotate(0f, spinAmount, 0f, Space.Self);
            yield return 0;
        }
        if (currentRotationAmount != yRotateAmount)
        {
            spinAmount = (yRotateAmount - currentRotationAmount);
            card.transform.Rotate(0f, spinAmount, 0f, Space.Self);
        }
    }
}
