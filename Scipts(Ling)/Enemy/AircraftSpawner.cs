using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftSpawner : MonoBehaviour
{
    [SerializeField]
    private int waveNo;
    public int WaveNo { get => waveNo; }
    [SerializeField]
    private string ObjectPoolName;

    private bool active;
    public bool Active { get => active; }

    private void Start()
    {
        active = true;
        GameManager._instance.RecordSpawner(this);
    }

    public void Spawn()
    {
        if (active)
        {
            ObjectPool pool = GameManager._instance.GetObjectPool(ObjectPoolName);
            //Debug.Log(name + ":" + pool);
            ObjectPoolUnit unit = pool.InitiateFromObjectPool(transform.position, transform.rotation);
            GameManager._instance.RecordEnemy(unit);
            GameManager._instance.CurWave = waveNo;
            active = false;
        }
    }
}
