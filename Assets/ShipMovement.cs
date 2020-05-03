using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShipMovement : MonoBehaviour
{
    [SerializeField] float shipRotationTime = 10f;

    float spinAmount;

    private void Start()
    {
        spinAmount = -(360f / shipRotationTime * Time.deltaTime);
    }

    void Update()
    {
        transform.RotateAround(new Vector3(0f, 0f, 0f), new Vector3(0f, 1f, 0f), spinAmount);
    }
}
