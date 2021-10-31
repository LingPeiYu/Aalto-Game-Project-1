using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TransportHelicopterAI : MonoBehaviour
{
    [SerializeField]
    private Health health;

    [Header("Systems")]
    [SerializeField]
    private HelicopterFlySystem helicopterFlySystem;
    [SerializeField]
    private SoldierSpawner soldierSpawner;

    [Header("Move")]
    [SerializeField]
    private string destinationSelecterName;
    [SerializeField]
    private float stopRadius;

    private Vector3 leavingPostion;//leaving position=starting position

    [SerializeField]
    private float landingHeight;
    [SerializeField]
    private float risingHeight;
    [SerializeField]
    private LayerMask landLayer;

    [SerializeField]
    private UnityEvent successfulLeavingEvents;

    private Transform landingDestination;

    private void Start()
    {
        soldierSpawner.TakeNewSoldiers();
        //landingDestination = GameManager._instance.GetTargetSelecter(destinationSelecterName).SelectTarget();
        leavingPostion = transform.position;
    }

    private void Update()
    {
        if (health.CurrentHealthPoint <= 0f) return;
        Vector3 toTarget;
        if(soldierSpawner.CurrentSoldierCount>0)
        {
            toTarget = landingDestination.position - transform.position;
            toTarget.y = 0f;
            if(toTarget.magnitude>stopRadius)//too far
            {
                //fly to the destination
                helicopterFlySystem.MoveForward();
                helicopterFlySystem.Rotate(toTarget);
            }
            else
            {
                if(!OnLand())//has landed
                {
                    //land
                    helicopterFlySystem.VerticalMove(false);
                }
                else
                {
                    //spawn soldiers
                    soldierSpawner.SpawnSoldiers();
                }

            }
        }
        else
        {
            toTarget = leavingPostion - transform.position;
            toTarget.y = 0f;
            if(IsRising())
            {
                //Move up and look at leaving position
                helicopterFlySystem.VerticalMove(true);
                helicopterFlySystem.Rotate(toTarget);
            }
            else
            {
                //Fly to the leaving position
                helicopterFlySystem.MoveForward();
                helicopterFlySystem.Rotate(toTarget);
                if (toTarget.magnitude <= stopRadius)//successful leaving
                {
                    successfulLeavingEvents.Invoke();
                    //Debug.Log("successful leaving");
                }
            }
        }
    }

    private bool OnLand()
    {
        return Physics.Raycast(transform.position, -transform.up, landingHeight, landLayer);
    }

    private bool IsRising()
    {
        return Physics.Raycast(transform.position, -transform.up, risingHeight, landLayer);
    }

    public void Init()
    {
        soldierSpawner.TakeNewSoldiers();
        landingDestination = GameManager._instance.GetTargetSelecter(destinationSelecterName).SelectTarget();
        leavingPostion = transform.position;
        health.AddHealthPoint(health.MaxHealthPoint);
    }
}
