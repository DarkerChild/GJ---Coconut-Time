using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PractiseTargetController : MonoBehaviour
{
    [SerializeField] int timeBeforeResetTargets = 2;

    [SerializeField] PairsPractiseTargets[] targetTemplates = null;
    [SerializeField] PairsPractiseTargets practiseTarget = null;

    public int noOfUnhitTargets;
    Transform[] targetPositionObjects;

    public bool reset = false;
    
    void Start()
    {
        noOfUnhitTargets = transform.childCount;
        SetupTargetPositions();
        CreateNewTargets();
    }

    private void Update()
    {
        if (reset)
        {
            reset = false;
            ResetTargets();
        }
    }

    private void SetupTargetPositions()
    {
        targetPositionObjects = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            targetPositionObjects[i] = transform.GetChild(i);
        }
    }

    private void CreateNewTargets()
    {
        foreach (Transform position in targetPositionObjects)
        {
            GameObject instTarget = targetTemplates[Random.Range(0, targetTemplates.Length)].gameObject;
            GameObject newTarget = Instantiate(instTarget, position.position, Quaternion.identity, position);
            if (newTarget.GetComponent<PairsPractiseTargets>().type == TargetType.wine)
            {
                newTarget.transform.position += new Vector3(0f, 0.09f, 0f);
            }
        }
    }

    private void ResetTargets()
    {
        DestroyAllTargets();
        CreateNewTargets();
        noOfUnhitTargets = transform.childCount;
    }

    public void DestroyAllTargets()
    {
        PairsPractiseTargets[] allTargets = FindObjectsOfType<PairsPractiseTargets>();
        print(allTargets.Length);
        foreach (PairsPractiseTargets target in allTargets)
        {
            Destroy(target.gameObject);
        }
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
        ResetTargets();
    }
}
