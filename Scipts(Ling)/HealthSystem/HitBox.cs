using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Deal Damage
[RequireComponent(typeof(Rigidbody))]//correctly test trigger
[RequireComponent(typeof(CapsuleCollider))]
public class HitBox : MonoBehaviour
{
    [SerializeField]
    private bool continuous = false;
    [SerializeField]
    private float damage;
    [SerializeField]
    private LayerMask layers;

    private Rigidbody rb;
    private Collider trigger;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb.useGravity) rb.useGravity = false;

         trigger = GetComponent<Collider>();
        if (!trigger.isTrigger) trigger.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!continuous)
        {
            if ((layers.value & (int)Mathf.Pow(2, other.gameObject.layer)) == (int)Mathf.Pow(2, other.gameObject.layer))// if the other's layer is included in layers
            {
                Vector3 toOther = other.transform.position - transform.position;
                HurtBox hurtBox;
                if (hurtBox = other.GetComponent<HurtBox>())
                {
                    DealDamage(hurtBox);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(continuous)
        {
            if ((layers.value & (int)Mathf.Pow(2, other.gameObject.layer)) == (int)Mathf.Pow(2, other.gameObject.layer))// if the other's layer is included in layers
            {
                Vector3 toOther = other.transform.position - transform.position;
                HurtBox hurtBox;
                if (hurtBox = other.GetComponent<HurtBox>())
                {
                    DealDamage(hurtBox);
                }
            }
        }
    }

    private void DealDamage(HurtBox hurtBox)
    {
        hurtBox.ReceiveDamage(damage);
    }
}
