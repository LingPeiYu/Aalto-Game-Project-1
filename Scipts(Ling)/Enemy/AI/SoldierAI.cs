using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierAI : MonoBehaviour
{
    [SerializeField]
    private Health health;

    [SerializeField]
    private string destinationSelecterName;

    [System.Serializable]
    private struct WeaponSystem
    {
        public float minRange;
        public float maxRange;
        public Weapon weapon;
        public int fireCount;
    }

    [Header("Systems")]
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private SoldierAim aim;
    [SerializeField]
    private WeaponSystem weaponSystem;

    [Header("Timer")]
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private float attackGapInMoving;
    [SerializeField]
    private float attackGapInDestination;

    [Header("Attack Check")]
    [SerializeField]
    private LayerMask layers;

    private bool isAttacking;
    private int fireCountDown;

    private Transform target;

    private Transform destination;

    private void Start()
    {
        destination = GameManager._instance.GetTargetSelecter(destinationSelecterName).SelectTarget();
        agent.SetDestination(destination.position);
        target = GameManager._instance.Player.transform;
        isAttacking = false;
        fireCountDown = 0;

        timer.ActivateTimer(attackGapInMoving);
    }

    private void Update()
    {
        if (health.CurrentHealthPoint <= 0f) return;
        weaponSystem.weapon.transform.LookAt(target);
        if (!agent.pathPending && isArrived() && timer.CurrentTimer > attackGapInDestination)
        {
            timer.ActivateTimer(attackGapInDestination);
            //Debug.Log(agent.remainingDistance + "<" + agent.stoppingDistance + "=" + (agent.remainingDistance < agent.stoppingDistance));
        }
        if (!isArrived() && agent.isStopped != isAttacking) agent.isStopped = isAttacking;
        if(isAttacking)
        {
            if(weaponSystem.weapon.IsCool())
            {
                weaponSystem.weapon.Fire();
                fireCountDown--;
            }
            if (fireCountDown <= 0)
                isAttacking = false;
        }
        aim.isAiming = isAttacking;
    }

    private bool isArrived()
    {
        return agent.remainingDistance <= agent.stoppingDistance;
    }

    public void Attack()
    {
        Vector3 toTarget = target.position - transform.position;
        toTarget.y = 0;
        if (toTarget.magnitude < weaponSystem.minRange || toTarget.magnitude > weaponSystem.maxRange
            || isAttacking)
            return;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, target.position - transform.position, out hit,weaponSystem.maxRange,layers))
        {
            if(hit.transform==target.transform)
            {
                isAttacking = true;
                fireCountDown = weaponSystem.fireCount;
            }
        }
    }

    public void Init()
    {
        destination = GameManager._instance.GetTargetSelecter(destinationSelecterName).SelectTarget();
        agent.SetDestination(destination.position);
        isAttacking = false;
        fireCountDown = 0;

        timer.ActivateTimer(attackGapInMoving);

        health.AddHealthPoint(health.MaxHealthPoint);
    }
}
