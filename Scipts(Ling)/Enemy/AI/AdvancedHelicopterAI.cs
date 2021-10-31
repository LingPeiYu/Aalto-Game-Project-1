using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedHelicopterAI : MonoBehaviour
{
    [SerializeField]
    private Health health;

    [System.Serializable]
    private struct WeaponSystem
    {
        public float minRange;
        public float maxRange;
        public float attackAngle;
        public Weapon weapon;
        public int fireCount;
    }

    [Header("Systems")]
    [SerializeField]
    private HelicopterFlySystem helicopterFlySystem;
    [SerializeField]
    private WeaponSystem[] weaponSystems;
    [SerializeField]
    private SoldierSpawner soldierSpawner;

    [Header("Timer")]
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private float attackGap;

    [Header("Wave")]
    [SerializeField]
    [Range(0, 1)]
    private float waveFrequency = 0.7f;

    [Header("Move")]
    [SerializeField]
    private string destinationSelecterName;
    [SerializeField]
    private float stopRadius;

    [SerializeField]
    private float landingHeight;
    [SerializeField]
    private float risingHeight;
    [SerializeField]
    private LayerMask landLayer;

    [SerializeField]
    [Range(0,1)]
    private float switchPosibility;


    private Transform target;
    private int currentWeapIndex;
    private int fireCountDown;

    private bool switchToTransport;

    private Transform landingDestination;

    private void Start()
    {
        switchToTransport = false;
        target = GameManager._instance.Player.transform;
        currentWeapIndex = -1;// no active weapon

        //landingDestination = GameManager._instance.GetTargetSelecter(destinationSelecterName).SelectTarget();
        soldierSpawner.TakeNewSoldiers();
    }

    private void Update()
    {
        if (health.CurrentHealthPoint <= 0f) return;
        Vector3 toTarget;
        if (!switchToTransport)
        {
            //Attack Mode
            toTarget = target.position - transform.position;
            toTarget.y = 0;
            bool isAttacking = false;
            if (currentWeapIndex == -1)
            {
                if (timer.IsZero())
                {
                    currentWeapIndex = Random.Range(0, weaponSystems.Length);
                    fireCountDown = weaponSystems[currentWeapIndex].fireCount;
                }
                else
                {
                    toTarget *= -1;
                }
            }
            else
            {
                weaponSystems[currentWeapIndex].weapon.transform.LookAt(target);
                if (toTarget.magnitude <= weaponSystems[currentWeapIndex].maxRange && toTarget.magnitude >= weaponSystems[currentWeapIndex].minRange
                    && fireCountDown > 0)
                {
                    isAttacking = true;
                    helicopterFlySystem.Rotate(toTarget);
                    if (Mathf.Abs(Vector3.Angle(transform.forward, toTarget)) <= weaponSystems[currentWeapIndex].attackAngle
                    && weaponSystems[currentWeapIndex].weapon.IsCool())
                    {
                        weaponSystems[currentWeapIndex].weapon.Fire();
                        fireCountDown--;
                    }
                }
                if (fireCountDown <= 0)
                {
                    currentWeapIndex = -1;
                    timer.ActivateTimer(attackGap);
                }
            }

            //Move
            if (!isAttacking)
            {
                helicopterFlySystem.MoveForward();
                helicopterFlySystem.Rotate(toTarget);
                if (Random.Range(0f, 1f) >= waveFrequency) helicopterFlySystem.WaveMove(Random.Range(0f, 1f) * Time.deltaTime);
            }
        }
        else
        {
            //Transport Mode
            if (soldierSpawner.CurrentSoldierCount > 0)
            {
                toTarget = landingDestination.position - transform.position;
                toTarget.y = 0f;
                if (toTarget.magnitude > stopRadius)//too far
                {
                    //fly to the destination
                    helicopterFlySystem.MoveForward();
                    helicopterFlySystem.Rotate(toTarget);
                }
                else
                {
                    if (!OnLand())//has landed
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
                toTarget = target.position - transform.position;
                toTarget.y = 0f;
                if (IsRising())
                {
                    //Move up and look at leaving position
                    helicopterFlySystem.VerticalMove(true);
                    helicopterFlySystem.Rotate(toTarget);
                }
                else
                {
                    switchToTransport = false;
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

    public void PossiblySwithcToTransport()
    {
        if (soldierSpawner.CurrentSoldierCount > 0 && Random.Range(0.0f, 1.0f) < switchPosibility) switchToTransport = true;
    }

    public void Init()
    {
        switchToTransport = false;
        currentWeapIndex = -1;// no active weapon

        landingDestination = GameManager._instance.GetTargetSelecter(destinationSelecterName).SelectTarget();
        soldierSpawner.TakeNewSoldiers();

        health.AddHealthPoint(health.MaxHealthPoint);
    }
}
