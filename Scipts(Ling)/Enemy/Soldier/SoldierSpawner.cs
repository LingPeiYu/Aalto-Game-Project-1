using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSpawner : MonoBehaviour
{
    [SerializeField]
    private string soldierPoolName;
    [SerializeField]
    private Transform[] getOffPoints;

    [SerializeField]
    private int maxSoldierCount;
    [SerializeField]
    private int maxOnceSpawnCount;
    [SerializeField]
    private float onceSpawnTime;
    [SerializeField]
    private Timer timer;

    private int currentSoldierCount;
    public int CurrentSoldierCount { get => currentSoldierCount; }

    public void SpawnSoldiers()
    {
        if (timer.IsZero())
        {
            for (int i = 0; i < maxOnceSpawnCount; i++)
            {
                if (currentSoldierCount <= 0) break;
                int pi = i % getOffPoints.Length;
                ObjectPoolUnit unit = GameManager._instance.GetObjectPool(soldierPoolName).InitiateFromObjectPool(getOffPoints[pi].position, getOffPoints[pi].rotation);
                GameManager._instance.RecordEnemy(unit);
                currentSoldierCount--;
            }

            timer.ActivateTimer(onceSpawnTime);
        }
    }

    public void TakeNewSoldiers()
    {
        currentSoldierCount = maxSoldierCount;
    }
}
