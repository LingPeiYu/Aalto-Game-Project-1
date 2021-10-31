using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : Weapon
{
    [HideInInspector]
    public bool launchMissle;

    [SerializeField]
    private int startMissileCount;

    private int curMissileCount;
    public int CurrentMissleCount { get => curMissileCount; }

    private void Start()
    {
        curMissileCount = startMissileCount;
    }

    public override void Fire()
    {
        if (curMissileCount > 0)
        {
            curMissileCount--;
            launchMissle = true;
            base.Fire();
        }
    }

    public void AddMissile(int count)
    {
        curMissileCount += count;
    }
}
