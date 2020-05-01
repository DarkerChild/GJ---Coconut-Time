using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Card : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        print(gameObject.name + "hit");
    }
}
