using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHelicopterAI : MonoBehaviour
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

    [Header("Timer")]
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private float attackGap;

    [Header("Wave")]
    [SerializeField]
    [Range(0, 1)]
    private float waveFrequency = 0.7f;

    private Transform target;
    private int currentWeapIndex;
    private int fireCountDown;

    private void Start()
    {
        target = GameManager._instance.Player.transform;
        currentWeapIndex = -1;// no active weapon
    }

    private void Update()
    {
        if (health.CurrentHealthPoint <= 0f) return;
        Vector3 toTarget = target.position - transform.position;
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
                && Mathf.Abs(Vector3.Angle(transform.forward, toTarget)) <= weaponSystems[currentWeapIndex].attackAngle
                && fireCountDown > 0)
            {
                isAttacking = true;
                if (weaponSystems[currentWeapIndex].weapon.IsCool())
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
        if(!isAttacking)
        {
            helicopterFlySystem.MoveForward();
            helicopterFlySystem.Rotate(toTarget);
            if (Random.Range(0f, 1f) >= waveFrequency) helicopterFlySystem.WaveMove(Random.Range(0f, 1f) * Time.deltaTime);
        }
    }

    public void Init()
    {
        currentWeapIndex = -1;
        health.AddHealthPoint(health.MaxHealthPoint);
    }
}
