using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Receive Damage
[RequireComponent(typeof(Rigidbody))]//correctly test trigger
[RequireComponent(typeof(CapsuleCollider))]
public class HurtBox : MonoBehaviour
{
    [SerializeField]
    private Health health;
    [SerializeField]
    private bool instantDeath;

    public void ReceiveDamage(float damage)
    {
        if (instantDeath) health.ReduceHealthPoint(health.MaxHealthPoint);
        else health.ReduceHealthPoint(damage);
    }

}
