using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelecter : MonoBehaviour
{
    [SerializeField]
    private List<Transform> targets;

    [SerializeField]
    private bool random;
    [SerializeField]
    private bool removeSelection;

    public Transform SelectTarget()
    {
        Transform target;
        if (random) target = targets[Random.Range(0, targets.Count)];
        else target = targets[0];

        if (removeSelection)
            targets.Remove(target);

        return target;
    }
}
