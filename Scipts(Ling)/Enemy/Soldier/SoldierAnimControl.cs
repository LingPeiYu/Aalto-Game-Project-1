using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierAnimControl : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Animator animator;

    private void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
