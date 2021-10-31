using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAI : MonoBehaviour
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
    private BasicFlySystem basicFlySystem;
    [SerializeField]
    private WeaponSystem[] weaponSystems;

    [Header("Timer")]
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private float attackGap;

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
        basicFlySystem.MoveForward();
        basicFlySystem.Rotate(toTarget);
        if (currentWeapIndex == -1)
        {
            if (timer.IsZero())
            {
                currentWeapIndex = Random.Range(0, weaponSystems.Length);
                fireCountDown = weaponSystems[currentWeapIndex].fireCount;
            }
        }
        else
        {
            weaponSystems[currentWeapIndex].weapon.transform.LookAt(target);
            if (toTarget.magnitude <= weaponSystems[currentWeapIndex].maxRange && toTarget.magnitude >= weaponSystems[currentWeapIndex].minRange
                && Mathf.Abs(Vector3.Angle(transform.forward, toTarget)) <= weaponSystems[currentWeapIndex].attackAngle
                && weaponSystems[currentWeapIndex].weapon.IsCool() 
                && fireCountDown > 0)
            {
                weaponSystems[currentWeapIndex].weapon.Fire();
                fireCountDown--;
            }
            if(fireCountDown<=0)
            {
                currentWeapIndex = -1;
                timer.ActivateTimer(attackGap);
            }
        }
    }

    public void Init()
    {
        currentWeapIndex = -1;
        health.AddHealthPoint(health.MaxHealthPoint);
    }
}
