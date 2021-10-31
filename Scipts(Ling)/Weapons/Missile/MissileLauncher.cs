using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField]
    private string missilePoolName;

    [SerializeField]
    private float lockAngle;

    private PlayerController player;
    private MissileController missileController;
    private List<ObjectPoolUnit> enemyUnits;
    private ObjectPool missilePool;

    private void Start()
    {
        missilePool = GameManager._instance.GetObjectPool(missilePoolName);
    }

    private void Update()
    {
        if (player == null) player = GameManager._instance.Player;
        if (missileController == null) missileController = player.GetComponentInChildren<MissileController>();
        if(missileController.launchMissle)
        {
            Fire();
            missileController.launchMissle = false;
        }
    }

    private void Fire()
    {
        ObjectPoolUnit target = LockTarget();
        if(target!=null)
        {
            transform.LookAt(target.transform.position);
        }
        else
        {
            transform.LookAt(player.transform.position + player.transform.forward * 1000);
        }

        Missile missile = missilePool.InitiateFromObjectPool(transform.position, transform.rotation).GetComponent<Missile>();
        missile.target = target;
        //Debug.Log(target);
    }


    private ObjectPoolUnit LockTarget()
    {
        if (enemyUnits == null) enemyUnits = GameManager._instance.EnemyUnits;

        Camera mainCamera = player.GetComponentInChildren<Camera>();

        float maxCos = -1;
        ObjectPoolUnit target = null;

        foreach(ObjectPoolUnit eu in enemyUnits)
        {
            if(eu.Active)
            {
                Vector3 toEnemy = (eu.transform.position - mainCamera.transform.position).normalized;
                float cos = Vector3.Dot(toEnemy, mainCamera.transform.forward);
                if (cos > Mathf.Cos(lockAngle) && cos > maxCos)
                {
                    maxCos = cos;
                    target = eu;
                }
            }
        }
        return target;
    }

}
