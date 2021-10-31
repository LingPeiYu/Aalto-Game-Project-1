using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserGun : Weapon
{
    [SerializeField]
    private Transform aimPoint;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float maxLength;
    [SerializeField]
    private HitScan hitScan;

    public override void Fire()
    {
        //Shoot
        base.Fire();
        RaycastHit hit;
        if (hitScan.EmitHitRay(aimPoint.transform.position, aimPoint.transform.forward,out hit))
        {
            target.position = hit.point;
        }
        else
        {
            target.position = aimPoint.transform.position + aimPoint.transform.forward * maxLength;
        }
    }
}
