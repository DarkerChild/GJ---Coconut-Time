using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PractiseTargetController : MonoBehaviour
{
    [SerializeField] int timeBeforeResetTargets = 2;

    PairsPractiseTargets[] allTargets;

    private int noOfUnhitTargets;
    
    void Start()
    {
        allTargets = FindObjectsOfType<PairsPractiseTargets>();
        noOfUnhitTargets = allTargets.Length;
    }

    public void TargetHit()
    {
        noOfUnhitTargets--;

        if (noOfUnhitTargets <= 0)
        {
            StartCoroutine(ResetPractiseTargets());
        }
    }

    IEnumerator ResetPractiseTargets()
    {
        yield return new WaitForSeconds(timeBeforeResetTargets);
        foreach (PairsPractiseTargets target in allTargets)
        {
            target.ResetPosition();
        }
    }
}
