using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : Weapon
{
    [SerializeField]
    private string bulletPoolName;

    [SerializeField]
    private int startBulletCount;
    private int currentBulletCount;
    public int CurrentBulletCount { get => currentBulletCount; }


    [SerializeField]
    private float bulletVelocity;

    private ObjectPool bulletPool;


    private void Start()
    {
        currentBulletCount = startBulletCount;
    }

    public override void Fire()
    {
        if (bulletPool == null) 
            bulletPool = GameManager._instance.GetObjectPool(bulletPoolName);
        if (currentBulletCount > 0)
        {
            ObjectPoolUnit bulletUnit = bulletPool.InitiateFromObjectPool(transform.position, transform.rotation);
            if (bulletUnit)
            {
                base.Fire();
                GameObject bullet = bulletUnit.gameObject;
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.velocity = bullet.transform.forward * bulletVelocity;
                currentBulletCount--;
            }
        }
    }

    public void AddBullet(int num)
    {
        currentBulletCount += num;
    }
}
