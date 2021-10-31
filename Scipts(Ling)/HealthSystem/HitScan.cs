using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScan : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private float maxDistance = 1000f;
    [SerializeField]
    private LayerMask layers;

    public bool EmitHitRay(Vector3 origin, Vector3 direction, out RaycastHit hitInfo)
    {
        RaycastHit hit;
        if(Physics.Raycast(origin,direction,out hit, maxDistance, layers))
        {
            hitInfo = hit;
            HurtBox hurtBox;
            if (hurtBox = hit.collider.GetComponent<HurtBox>())
            {
                DealDamage(hurtBox);
                return true;
            }
        }
        hitInfo = hit;
        return false;
    }

    private void DealDamage(HurtBox hurtBox)
    {
        hurtBox.ReceiveDamage(damage);
    }
}
